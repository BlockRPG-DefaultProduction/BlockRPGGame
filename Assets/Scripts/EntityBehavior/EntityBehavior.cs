using System;
using UnityEngine;
public class EntityBehavior : MonoBehaviour
{
    // THIS SHOULD BE A SCRIPTABLEOBJECT, WHY?
    // its okay
    // Entity Attributes:
    public float velocity = 5f;
    public float rotationSpeed = 180f; // Độ xoay mỗi giây
    public int health = 1;
    private bool isDead = false;

    // Entity States:
    public Vector2Int gridPosition = new Vector2Int(0, 0); // (row, col) trong grid
    public Vector2Int direction = Vector2Int.up; // Hướng mặc định: tiến lên (Y+)
    public bool isAtTurn = false; // Biến để kiểm tra xem entity có đang ở lượt của nó hay không
    public Vector2Int selectedTilePosition = new Vector2Int(0, 0); // Vị trí ô được chọn

    // Entity Components:
    public BattleManager battleManager;
    public Animator entityAnimation;
    private MapManager map;
    public event Action NextTurn;
    
    void Awake()
    {

    }
    void Start()
    {
        map = FindFirstObjectByType<MapManager>();

        if (entityAnimation == null)
        {
            entityAnimation = GetComponentInChildren<Animator>();
            if (entityAnimation == null)
            {
            Debug.LogError("EntityAnimation component not found!");
            }
        }
    }
    // Entity Turn Basic Logic:
    // This is seperate from action logic, which is handled by EntityTileActionManager
    void Update()
    {
        if (isAtTurn)
        {
            AtTurn();
        }

        if (health <= 0 && !isDead)
        {
            isDead = true; // Set isDead to true to prevent multiple calls
            OnDeath();         
            
        }
    }

    public virtual void StartTurn()
    {
        isAtTurn = true;
    }

    public virtual void AtTurn()
    {

    }

    public virtual void CompleteTurn()
    {
        isAtTurn = false;
        NextTurn?.Invoke();
    }

    public virtual void OnDeath()
    {
        int removedIndex = battleManager.entities.IndexOf(this);
        battleManager.entities.Remove(this);

        if (battleManager.turnCount >= removedIndex)
        {
        battleManager.turnCount--;
        }
        Destroy(gameObject); // Destroy the entity after 2 seconds
    }
}