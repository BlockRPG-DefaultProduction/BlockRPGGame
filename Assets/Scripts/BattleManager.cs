using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public int gridSize = 5;  // Kích thước lưới, ví dụ 4 hoặc 5
    public int turnCount = -1; // Số lượt đã qua
    public GameObject[,] grid; // Lưới chứa các ô

    public List<EntityBehavior> entities = new List<EntityBehavior>();
    public EntityBehavior activeEntity; // Thực thể đang hoạt động trong lượt hiện tại

    void Awake()
    {

        grid = new GameObject[gridSize, gridSize];
        NextTurn(); // Bắt đầu lượt đầu tiên
        activeEntity.NextTurn += NextTurn; // Subscribe to the NextTurn event
    }

    public void NextTurn()
    {
        turnCount++;
        Debug.Log($"Turn {turnCount + 1} started.");
        if (turnCount >= entities.Count)
        {
            turnCount = 0; // Reset turn count if it exceeds the number of entities
        } 

        activeEntity = entities[turnCount];
        activeEntity.StartTurn();
    }
}