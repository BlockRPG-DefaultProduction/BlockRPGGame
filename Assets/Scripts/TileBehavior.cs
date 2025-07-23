using UnityEngine;
using UnityEngine.EventSystems;
public class TileBehavior : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{

    public bool canBeDuplicated = true;
    public int id = 0;

    private TileActionManager tileActionManager;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        tileActionManager = FindFirstObjectByType<TileActionManager>(); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canBeDuplicated)
        {
            if (canvas == null)
            {
                canvas = GetComponentInParent<Canvas>();
                if (canvas == null)
                {
                    Debug.LogError("Canvas not found in parent hierarchy.");
                    return;
                }
            }
            id++;
            GameObject instance = Instantiate(gameObject);
            instance.name = "Tile" + id;
            instance.transform.SetParent(transform.parent, false);
            instance.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
            instance.GetComponent<TileBehavior>().canBeDuplicated = true;
            canBeDuplicated = false;
        }
        transform.SetAsLastSibling(); // Bring the tile to the front
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == null ||
            eventData.pointerCurrentRaycast.gameObject.GetComponent<PlacementBarBehavior>() == null)
        {
            tileActionManager.RemoveTile(gameObject);
            Destroy(gameObject);
        }
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canBeDuplicated)
        {
            PlacementBarBehavior placementBar = FindFirstObjectByType<PlacementBarBehavior>();
            if (placementBar != null)
            {
                if (canvas == null)
                {
                    canvas = GetComponentInParent<Canvas>();
                    if (canvas == null)
                    {
                        Debug.LogError("Canvas not found in parent hierarchy.");
                        return;
                    }
                }
                id++;
                GameObject instance = Instantiate(gameObject);
                instance.name = "Tile" + id;
                instance.transform.SetParent(transform.parent, false);
                TileBehavior instanceTileBehavior = instance.GetComponent<TileBehavior>();
                instanceTileBehavior.canBeDuplicated = false;
                tileActionManager.AddTile(instance);
                instanceTileBehavior.Initialize();
            }
        }
    }
}
