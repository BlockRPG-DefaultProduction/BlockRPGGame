using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
public class PlacementBarBehavior : MonoBehaviour, IDropHandler
{
    public float tileWidth = 64f;
    public int limit = 5;
    private RectTransform rectTransform;
    private PlayerTileActionManager tileActionManager;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        tileActionManager = FindFirstObjectByType<PlayerTileActionManager>();
        tileActionManager.OnTileListChange += RerenderGameObjectOnListChange;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedTile = eventData.pointerDrag;
        if (droppedTile != null)
        {
            RectTransform droppedRectTransform = droppedTile.GetComponent<RectTransform>();

            TileBehavior tileBehavior = droppedTile.GetComponent<TileBehavior>();
            if (droppedRectTransform == null || tileActionManager == null)
            {
                Debug.LogError("Dropped tile or TileActionManager not found.");
                return;
            }
            if (tileBehavior != null)
            {
                tileActionManager.AddTile(droppedTile);
            }
            RerenderGameObjectOnListChange(); // Rerender tile anyway
        }
    }

    private void RerenderGameObjectOnListChange()
    {
        foreach (GameObject tile in tileActionManager.tileToExecute)
        {
            if (tile != null)
            {
                RectTransform tileRectTransform = tile.GetComponent<RectTransform>();
                if (tileRectTransform != null)
                {
                    float offset = tileWidth * tileActionManager.tileToExecute.IndexOf(tile);
                    tileRectTransform.anchoredPosition = rectTransform.anchoredPosition + new Vector2(offset, 0);
                }
            }
        }
    }
}
