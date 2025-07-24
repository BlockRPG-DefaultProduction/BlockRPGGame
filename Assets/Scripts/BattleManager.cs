using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public int gridSize = 5;  // Kích thước lưới, ví dụ 4 hoặc 5
    public int turnCount = 0; // Số lượt đã qua

    public GameObject[,] grid; // Lưới chứa các ô

    void Awake()
    {
        grid = new GameObject[gridSize, gridSize];
    }
}