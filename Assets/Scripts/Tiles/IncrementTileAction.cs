using UnityEngine;

public class IncrementTileAction : AbstractTileAction
{
    public MapManager mapManager; // Tham chiếu đến MapManager

    void Awake()
    {
        mapManager = FindFirstObjectByType<MapManager>();
        if (mapManager == null)
        {
            Debug.LogError("MapManager not found in the scene!");
        }
    }
    public override void Invoke()
    {
        base.Invoke();
        GameObject tile = mapManager.GetTileAt(entity.gridPosition.x, entity.gridPosition.y);
        VariableManager variableManager = tile.GetComponentInChildren<VariableManager>();
        variableManager.Increment();
        Complete();
    }
}
