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
            if (target.GetComponent<EntityBehavior>().health <= 0)
            {
                Explode(); // Explode if health is zero or less
                Complete();
            }
        }
    }

    public override void Action()
    {

    }
    public void Explode()
    {
        Vector2Int pos = target.GetComponent<EntityBehavior>().direction;
        battleManager.grid[pos.x, pos.y] = null; // Clear the grid position
        battleManager.entities.Remove(target.GetComponent<EntityBehavior>()); // Remove the target from the battle manager

        Vector3 explosionPosition = target.transform.position;
        DetachAndExplode(target.transform, target.transform.position); // Detach and explode the target
        Destroy(target); // Destroy the target object
    }
    
    private void DetachAndExplode(Transform root, Vector3 explosionPos)
{
        foreach (Transform child in root)
        {
            child.SetParent(null);

            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (child.GetComponent<Collider>() == null)
                {
                    // Add a collider if it doesn't have one
                    child.gameObject.AddComponent<BoxCollider>();
                }
                rb.isKinematic = false; // Make the rigidbody dynamic
                rb.AddExplosionForce(500f, explosionPos, 5f); // Apply explosion
            }

            // Recursively apply to all descendants
            DetachAndExplode(child, explosionPos);
            Destroy(child.gameObject, 2f); // Destroy after 2 seconds
        }
    }

}
