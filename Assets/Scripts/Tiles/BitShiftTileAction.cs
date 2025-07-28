using System;
using UnityEngine;

public class BitShiftTileAction : AbstractTileAction
{
    public GameObject target;
    public int attackPower = 1; // Sức mạnh tấn công
    public override void Invoke()
    {
        base.Invoke();
        int targetX = entity.gridPosition.x + entity.direction.x;
        int targetY = entity.gridPosition.y + entity.direction.y;

        // Check bounds
        if (targetX < 0 || targetX >= battleManager.grid.GetLength(0) ||
            targetY < 0 || targetY >= battleManager.grid.GetLength(1))
        {
            Debug.Log("Target position is out of bounds.");
            Complete();
            return;
        }

        target = battleManager.grid[targetX, targetY];

        if (target == null)
        {
            Debug.Log("No target to attack.");
            Complete();
        }
        else if (target.GetComponent<EntityBehavior>() == null)
        {
            Debug.Log("Target is not an entity.");
            Complete();
        }
        else
        {
            Debug.Log($"Attacking {target.name} with power {attackPower}");
            target.GetComponent<EntityBehavior>().health -= attackPower; // Reduce health of the target
            Complete();
        }
    }
    

    public override void Complete()
    {
        // if (target != null && target.GetComponent<EntityBehavior>() != null)
        // {
        //     if (target.GetComponent<EntityBehavior>().health <= 0)
        //     {
        //         Debug.Log($"{target.name} has been defeated.");
        //         battleManager.entities.Remove(target.GetComponent<EntityBehavior>());
        //         Destroy(target, 2f);
        //     }
        // }
        base.Complete();
    }


}
