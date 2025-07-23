using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileActionManager : MonoBehaviour
{
    public event Action OnTileListChange;
    public List<GameObject> tileToExecute = new List<GameObject>();

    // Tile List Adders and Removers Methods
    public void AddTile(GameObject tile)
    {
        if (tile == null || tileToExecute.Contains(tile))
        {
            Debug.LogWarning("Tile is null or already exists in the list.");
            return;
        }
        tileToExecute.Add(tile);
        OnTileListChange?.Invoke();
    }
    public void RemoveTile(GameObject tile)
    {
        if (tileToExecute.Contains(tile))
        {
            tileToExecute.Remove(tile);
            OnTileListChange?.Invoke();
        }
    }
    public void ClearTile(bool shouldDestroy = false)
    {
        if (shouldDestroy)
        {
            foreach (var tile in tileToExecute)
            {
                Destroy(tile);
            }
        }
        tileToExecute.Clear();
        OnTileListChange?.Invoke();
    }
    public void ExecuteAction()
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
            action.Invoke();
            yield return new WaitUntil(() => !action.isExecuting);
        }
        ClearTile(true);
    }
    
}
