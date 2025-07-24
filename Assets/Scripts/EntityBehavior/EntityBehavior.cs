using UnityEngine;
public class EntityBehavior : MonoBehaviour
{
    // Entity Attributes:
    public float velocity = 5f;
    public float rotationSpeed = 180f; // Độ xoay mỗi giây

    // Entity States:
    public Vector2Int gridPosition = new Vector2Int(0, 0); // (row, col) trong grid
    public Vector2Int direction = Vector2Int.up; // Hướng mặc định: tiến lên (Y+)
    public bool isAtTurn = false; // Biến để kiểm tra xem entity có đang ở lượt của nó hay không

    // Entity Components:
    public Animator entityAnimation;
    private MapManager map;
    
    void Start()
    {
        map = FindFirstObjectByType<MapManager>();

        entityAnimation = GetComponentInChildren<Animator>();
        if (entityAnimation == null)
        {
            Debug.LogError("EntityAnimation component not found!");
        }
    }
    // Entity Turn Basic Logic:
    void Update()
    {
        if (isAtTurn)
        {
            AtTurn();
        }
    }

    public virtual void StartTurn()
    {
        isAtTurn = true;
    }

    public virtual void AtTurn()
    {

    }

    public virtual void CompleteTurn()
    {
        isAtTurn = false;
    }
}