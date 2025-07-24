using UnityEngine;
public class PlayerBehavior : EntityBehavior
{
    public override void AtTurn()
    {
        // Player-specific turn logic can be implemented here
        CompleteTurn();
    }
}