using UnityEngine;
public class VariableTileAction : AbstractTileAction
{
    public MapManager mapManager; // Tham chiếu đến MapManager
    public override void Invoke()
    {
        base.Invoke();
        var tile = mapManager?.GetTileAt(entity.gridPosition.x, entity.gridPosition.y);
        var variableManager = tile.GetComponentInChildren<VariableManager>();
        variableManager.Initialize();
        Complete();
    }
}