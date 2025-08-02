using Unity.Mathematics;
using UnityEngine;

public abstract class AbstractSelectTileAction : AbstractTileAction
{
    public MapManager mapManager; // Reference to MapManager
    public VariableManager variableManager; // Reference to VariableManager
    public Vector2Int SelectedTilePosition; // Position of the selected tile
    public int variable = 0;

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
        Select();
    }

    public void GetNumber()
    {
        var tile = mapManager.GetTileAt(SelectedTilePosition.x, SelectedTilePosition.y);
        variableManager = tile.GetComponentInChildren<VariableManager>();
        variable = variableManager.Variable;
    }

    public virtual void Select()
    {
        SelectedTilePosition = entity.selectedTilePosition;
    }
    public virtual void ConsumeNumber()
    {
        variableManager.Clear();
        variable = 0;
    }
}