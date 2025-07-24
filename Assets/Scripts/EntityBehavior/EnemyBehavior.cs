using UnityEngine;

public class EnemyBehavior : EntityBehavior
{
    public override void AtTurn()
    {
        CompleteTurn();
    }
}