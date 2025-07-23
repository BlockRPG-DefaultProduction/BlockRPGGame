using System.Collections;
using UnityEngine;
using static Utilities.VectorHelper;

public class MoveTileAction : AbstractTileAction
{
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private float travelTime;
    private float startTime;

    [SerializeField] private MapManager map;
    public override void Invoke()
    {
        base.Invoke();
        Vector2Int nextPos = player.gridPosition + player.direction;
        GameObject nextTile = map.GetTileAt(nextPos.x, nextPos.y);
        if (nextTile != null)
        {
            startPosition = player.transform.position;
            targetPosition = CorrectOffsetPosition(nextTile.transform.position, 1f);

            startTime = Time.time;
            travelTime = Vector3.Distance(startPosition, targetPosition) / player.velocity;

            player.gridPosition = nextPos;
            player.playerAnimation.SetBool("IsMoving", true);
        }
        else
        {
            Debug.Log("Invalid move. Outside map.");
            Complete();
        }
        
    }
    public override void Action()
    {
        float elapsedTime = Time.time - startTime;
        float t = elapsedTime / travelTime;
        player.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
        if (t >= 1f)
        {
            player.transform.position = targetPosition;
            player.playerAnimation.SetBool("IsMoving", false);
            Complete();
        }
    }
}
