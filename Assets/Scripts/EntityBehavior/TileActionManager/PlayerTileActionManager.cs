using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTileActionManager : EntityTileActionManager
{
    // Player Specific Tile Action Manager
    // Inherits from EntityTileActionManager to manage player-specific tile actions
    public bool canBeAdded = false; // Biến để kiểm tra xem có thể thêm hành động vào danh sách hay không
    public int limit = 5;
    public event Action OnTileListChange;

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
}
