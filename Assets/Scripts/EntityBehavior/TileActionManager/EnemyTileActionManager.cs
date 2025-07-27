using UnityEngine;
using System.Collections.Generic;

public class EnemyTileActionManager : EntityTileActionManager
{
    public void ClearActions(bool shouldDestroy = false)
    {
        if (shouldDestroy)
        {
            foreach (var tile in tileToExecute)
            {
                Destroy(tile);
            }
        }
        tileToExecute.Clear();
        Debug.Log("All actions cleared.");
    }
}
