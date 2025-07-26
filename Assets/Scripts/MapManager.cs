using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utilities.VectorHelper;
public class MapManager : MonoBehaviour
{
    public GameObject groundTilePrefab;             // Prefab mặt đất
    public GameObject alternativeGroundTilePrefab; // Prefab mặt đất thay thế
    public GameObject enemyPrefab;             // Prefab kẻ thù
    public BattleManager battleManager; // Tham chiếu đến BattleManager
    private int gridSize;          // Kích thước lưới, mặc định là 5x5
    public float tileSpacing = 1.1f;     // Khoảng cách giữa các tile
    public float timeToGenerate = 0.01f; // Thời gian tạo tile

    private List<GameObject> groundTiles = new List<GameObject>();
    private PlayerBehavior player;        // Tham chiếu đến Player

    void Start()
    {
        player = FindFirstObjectByType<PlayerBehavior>();
        GenerateGrid();
        PlacePlayer(); // Đặt player sau khi sinh xong map
        PlacePlaceholderEnemy(3);
    }

    void Awake()
    {
        gridSize = battleManager.gridSize;
        if (gridSize <= 0)
        {
            Debug.LogError("Grid size must be greater than 0. Using default size of 5.");
            gridSize = 5; // Đặt kích thước lưới mặc định nếu không được chỉ định
        }
    }

    void GenerateGrid()
    {
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                Vector3 position = new Vector3(col * tileSpacing, 0, row * tileSpacing);
                InstantiateTile(position, row, col);
            }
        }
        
    }

    // Little Instantiate Animation
    // Should this be needed?
    void InstantiateTile(Vector3 position, int row, int col)
    {
        GameObject tile;
        if ((row + col) % 2 == 0)
        {
            tile = Instantiate(alternativeGroundTilePrefab, position, Quaternion.identity, transform);
        } else {
            tile = Instantiate(groundTilePrefab, position, Quaternion.identity, transform);
        }
        tile.name = $"Tile_{row}_{col}";
        tile.transform.SetParent(transform.GetChild(1));
        groundTiles.Add(tile);
    }

    // Đặt Player lên tile ban đầu
    void PlacePlayer()
    {
        if (player == null) return;
        GameObject startTile = GetTileAt(player.gridPosition.x, player.gridPosition.y);
        if (startTile != null)
        {
            player.transform.SetPositionAndRotation(
                CorrectOffsetPosition(startTile.transform.position, 1f),
                Quaternion.LookRotation(RotationCorrection(new Vector3(player.direction.x, 0, player.direction.y)))
            );
            battleManager.grid[player.gridPosition.x, player.gridPosition.y] = player.gameObject;
            battleManager.entities.Add(player);
            player.NextTurn += battleManager.NextTurn; // Subscribe to the NextTurn event
        }
        else
        {
            Debug.LogError("Start tile does not exist. Check gridPosition.");
        }
    }

    void PlacePlaceholderEnemy(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2Int randomPos = new Vector2Int(Random.Range(0, gridSize), Random.Range(0, gridSize));
            // Kiểm tra xem ô đã có kẻ thù hay chưa
            while (battleManager.grid[randomPos.x, randomPos.y] != null)
            {
                randomPos = new Vector2Int(Random.Range(0, gridSize), Random.Range(0, gridSize));
            }
            GameObject tile = GetTileAt(randomPos.x, randomPos.y);
            if (tile != null)
            {
                GameObject enemy = Instantiate(enemyPrefab, CorrectOffsetPosition(tile.transform.position, 1f), Quaternion.identity, transform);
                enemy.transform.SetParent(transform.GetChild(0));
                enemy.name = $"Enemy_{randomPos.x}_{randomPos.y}";
                enemy.GetComponent<EnemyBehavior>().gridPosition = randomPos;
                battleManager.grid[randomPos.x, randomPos.y] = enemy;
                battleManager.entities.Add(enemy.GetComponent<EnemyBehavior>());
                enemy.GetComponent<EnemyBehavior>().NextTurn += battleManager.NextTurn; // Subscribe to the NextTurn event
            }
        }
    }

    public GameObject GetTileAt(int row, int col)
    {
        if (row < 0 || col < 0 || row >= gridSize || col >= gridSize) return null;
        int index = row * gridSize + col;
        if (index < 0 || index >= groundTiles.Count) return null;
        return groundTiles[index];
    }
}