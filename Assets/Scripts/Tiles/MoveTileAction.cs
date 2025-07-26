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
        Vector2Int nextPos = entity.gridPosition + entity.direction;
        GameObject nextTile = map.GetTileAt(nextPos.x, nextPos.y);
        if (nextTile != null)
        {
            startPosition = entity.transform.position;
            targetPosition = CorrectOffsetPosition(nextTile.transform.position, 1f);

            startTime = Time.time;
            travelTime = Vector3.Distance(startPosition, targetPosition) / entity.velocity;

            battleManager.grid[entity.gridPosition.x, entity.gridPosition.y] = null;
            entity.gridPosition = nextPos;
            entity.entityAnimation.SetBool("IsMoving", true);
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
        entity.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
        if (t >= 1f)
        {
            entity.transform.position = targetPosition;
            entity.entityAnimation.SetBool("IsMoving", false);
            Complete();
        }
    }

    public override void Complete()
    {
        battleManager.grid[entity.gridPosition.x, entity.gridPosition.y] = entity.gameObject;   
        base.Complete();
    }
}
