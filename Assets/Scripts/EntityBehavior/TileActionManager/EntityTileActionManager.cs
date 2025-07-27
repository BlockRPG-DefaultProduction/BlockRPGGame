using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EntityTileActionManager is responsible for managing tile actions for entities.
/// This is based on the PlayerTileActionManager but adapted for entities.
/// PTAM will be inherited from this class.
/// </summary>
public class EntityTileActionManager : MonoBehaviour
{
    public event Action exectionFinished; // Event to notify when an action is finished
    public List<GameObject> tileToExecute = new(); // Tile as game objects
    public EntityBehavior entityBehavior; // Reference to the entity behavior, can be PlayerBehavior or any other entity type

    public virtual void ExecuteAction()
    {
        if (tileToExecute.Count == 0)
        {
            Debug.LogWarning("No actions to execute.");
            return;
        }

        StartCoroutine(ExecuteActionsCoroutine());
    }
    IEnumerator ExecuteActionsCoroutine()
    {
        foreach (var tile in tileToExecute)
        {
            AbstractTileAction action = tile.GetComponent<AbstractTileAction>();
            action.entity = entityBehavior;
            action.battleManager = entityBehavior.battleManager;
            action.Invoke();
            yield return new WaitUntil(() => !action.isExecuting);
        }
        exectionFinished?.Invoke();
    }

}
