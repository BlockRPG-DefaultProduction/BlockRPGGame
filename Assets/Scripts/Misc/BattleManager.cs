using System.Collections;
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
    }
    void Start()
    {
        StartCoroutine(DelayedInit());
    }
    private IEnumerator DelayedInit()
    {
        yield return new WaitForSeconds(1f); // Đợi một khung hình để đảm bảo các đối tượng đã được khởi tạo
        if (entities.Count > 0)
        {
            NextTurn();
        }
    }
    public void NextTurn()
    {
        turnCount++;
        Debug.Log($"Turn {turnCount + 1} started.");
        activeEntity = entities[turnCount % entities.Count];
        activeEntity.StartTurn();
    }
}