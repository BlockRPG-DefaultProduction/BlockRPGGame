using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTileActionManager : MonoBehaviour
{
    public event Action exectionFinished; // Event to notify when an action is finished
    public bool canBeAdded = false; // Biến để kiểm tra xem có thể thêm hành động vào danh sách hay không
    public int limit = 5;
    public event Action OnTileListChange;
    public List<GameObject> tileToExecute = new List<GameObject>();
    public PlayerBehavior playerBehavior;
    

    // Tile List Adders and Removers Methods
    public void AddTile(GameObject tile)
    {
        if (canBeAdded)
        {
            if (tile == null || tileToExecute.Contains(tile))
            {
                Debug.LogWarning("Tile is null or already exists in the list.");
                return;
            }
            if (tileToExecute.Count >= limit)
            {
                Debug.LogWarning("Tile limit reached, cannot add more tiles.");
                Destroy(tile);
                return;
            }
            tileToExecute.Add(tile);
            OnTileListChange?.Invoke();
        }
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

    [Obsolete("This is a dumb idea in general. What? Invoke Action directly? Too bad enemy didnt have that luxury.")]
    // Ignore the obsolete, new monobehavior is fine
    public void ExecuteAction()
    {
        if (tileToExecute.Count == 0)
        {
            Debug.LogWarning("No actions to execute.");
            return;
        }

        StartCoroutine(ExecuteActionsCoroutine());
    }
    [Obsolete("This is a dumb idea in general. What? Invoke Action directly? Too bad enemy didnt have that luxury.")]
    IEnumerator ExecuteActionsCoroutine()
    {
        foreach (var tile in tileToExecute)
        {
            AbstractTileAction action = tile.GetComponent<AbstractTileAction>();
            action.entity = playerBehavior;
            action.battleManager = playerBehavior.battleManager;
            action.Invoke();
            yield return new WaitUntil(() => !action.isExecuting);
        }
        exectionFinished?.Invoke();
    }
    
}
