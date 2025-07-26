using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : EntityBehavior
{
    private List<AbstractTileAction[]> enemyActions = new List<AbstractTileAction[]>();
    public override void StartTurn()
    {
        base.StartTurn();
        enemyActions.Clear();

        enemyActions.Add(new AbstractTileAction[3]);
        for (int i = 0; i < 3; i++)
        {
            enemyActions[0][i] = new MoveTileAction
            {
                entity = this,
                battleManager = battleManager
            };
        }

    }
    public override void AtTurn()
    {
        for (int i = 0; i < enemyActions.Count; i++)
        {
            StartCoroutine(ExecuteEnemyActions(enemyActions[i]));
        }
        CompleteTurn();
    }

    private IEnumerator ExecuteEnemyActions(AbstractTileAction[] actions)
    {
        foreach (var action in actions)
        {
            action.Invoke();
            yield return new WaitUntil(() => !action.isExecuting);
        }
    }
}