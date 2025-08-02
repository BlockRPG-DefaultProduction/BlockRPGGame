public class SelectStandingTileAction : AbstractSelectTileAction
{
    public override void Invoke()
    {
        base.Invoke();
        SelectedTilePosition = entity.selectedTilePosition;
    }

    public override void Select()
    {
        entity.selectedTilePosition = entity.gridPosition; // Set the selected tile position to the entity's current grid position
        base.Select(); // Call the base method to ensure any additional logic is executed
    }
}