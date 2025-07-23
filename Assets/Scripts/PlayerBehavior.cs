using System;
using UnityEngine;
using static Utilities.VectorHelper;

public class PlayerBehavior : MonoBehaviour
{
    public float velocity = 5f;
    public float rotationSpeed = 180f; // Độ xoay mỗi giây
    public Vector2Int gridPosition = new Vector2Int(0, 0); // (row, col) trong grid
    public Vector2Int direction = Vector2Int.up; // Hướng mặc định: tiến lên (Y+)
    private Animator playerAnimation;

    private State currentState = State.Idle;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float travelTime;
    private float startTime;
    private float rotateAngle;

    public bool isExecuting = false;
    private MapManager map;

    // Xóa logic đặt vị trí khỏi Start
    void Start()
    {
        map = FindFirstObjectByType<MapManager>();

        playerAnimation = GetComponent<Animator>();
        if (playerAnimation == null)
        {
            Debug.LogError("PlayerAnimation component not found!");
        }
    }

    void Update()
    {
        if (currentState == State.Moving)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / travelTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            if (t >= 1f)
            {
                currentState = State.Idle;
                isExecuting = false;
                transform.position = targetPosition; // Snap to tile center
                Debug.Log("Finished moving to: " + gridPosition);
                playerAnimation.SetBool("IsMoving", false);
            }
        }
        else if (currentState == State.Rotating)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / travelTime;
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * rotateAngle / Mathf.Abs(rotateAngle));
            if (t >= 1f)
            {
                currentState = State.Idle;
                isExecuting = false;
                transform.rotation = Quaternion.LookRotation(RotationCorrection(new Vector3(direction.x, 0, direction.y)));
                Debug.Log("Finished rotating to direction: " + direction);
            }
        }
    }
    [Obsolete("Reimplementation in the tile action system")]
    public void Move()
    {
        if (currentState == State.Moving || isExecuting) return;

        Vector2Int nextPos = gridPosition + direction;
        GameObject nextTile = map.GetTileAt(nextPos.x, nextPos.y);

        if (nextTile != null)
        {
            isExecuting = true;
            currentState = State.Moving;
            startPosition = transform.position;
            targetPosition = CorrectOffsetPosition(nextTile.transform.position, 1f); // Offset để di chuyển đến giữa tile
            travelTime = Vector3.Distance(startPosition, targetPosition) / velocity;
            startTime = Time.time;
            gridPosition = nextPos; // Cập nhật vị trí grid
            playerAnimation.SetBool("IsMoving", true); // Bật animation di chuyển
        }
        else
        {
            Debug.Log("Invalid move. Outside map.");
        }
    }

    [Obsolete("Reimplementation in the tile action system")]
    public void Rotate(bool counterClockwise)
    {
        if (currentState == State.Moving || isExecuting) return;
        isExecuting = true;
        currentState = State.Rotating;
        rotateAngle = (float)(counterClockwise ? Math.PI / 2f : -Math.PI / 2f);
        startTime = Time.time;
        travelTime = Mathf.Abs((float)((180 / Math.PI) * rotateAngle) / rotationSpeed); // Thời gian xoay
        direction = Rotation2i(direction, rotateAngle);
        Debug.Log("Rotated to direction: " + direction);
    }

    // Hàm này để MapManager gọi khi đặt player lên tile ban đầu
    public void SetPlayerPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
    }

}

[Obsolete("Reimplementation in the tile action system")]
enum State
{
    Idle,
    Moving,
    Rotating
}