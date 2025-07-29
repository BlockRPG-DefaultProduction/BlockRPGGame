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

    void Awake()
    {
        map = FindFirstObjectByType<MapManager>();
        if (map == null)
        {
            Debug.LogError("MapManager not found in the scene!");
        }
    }
    public override void Invoke()
    {
        base.Invoke();
        Vector2Int nextPos = entity.gridPosition + entity.direction;
        GameObject nextTile = map.GetTileAt(nextPos.x, nextPos.y);
        if (nextTile != null)
        {
            if (battleManager.grid[nextPos.x, nextPos.y] != null)
            {
                Debug.Log("Invalid move. Tile occupied by another entity.");
                Complete();
                return;
            }
            entity.entityAnimation.SetBool("IsMoving", true);
            startPosition = entity.transform.position;
            targetPosition = CorrectOffsetPosition(nextTile.transform.position, 1f);

            startTime = Time.time;
            travelTime = Vector3.Distance(startPosition, targetPosition) / entity.velocity;

            battleManager.grid[entity.gridPosition.x, entity.gridPosition.y] = null;
            entity.gridPosition = nextPos;
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
            
            Complete();
        }
    }

    public override void Complete()
    {
        entity.entityAnimation.SetBool("IsMoving", false);
        battleManager.grid[entity.gridPosition.x, entity.gridPosition.y] = entity.gameObject;   
        base.Complete();
    }
}
