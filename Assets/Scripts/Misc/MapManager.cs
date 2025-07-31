using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utilities.VectorHelper;
public class MapManager : MonoBehaviour
{
    public StageSetting stageSetting; // Tham chiếu đến StageSetting
    public BattleManager battleManager; // Tham chiếu đến BattleManager
    private List<GameObject> groundTiles = new List<GameObject>();
    private PlayerBehavior player;        // Tham chiếu đến Player
    private int gridSize; // Kích thước lưới
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
                Vector3 position = new Vector3(col * 2 + stageSetting.tileSpacing, 0, row * 2 + stageSetting.tileSpacing);
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
            tile = Instantiate(stageSetting.alternativeGroundTilePrefab, position, Quaternion.identity, transform);
        } else {
            tile = Instantiate(stageSetting.groundTilePrefab, position, Quaternion.identity, transform);
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
                Quaternion.LookRotation(new Vector3(player.direction.x, 0, player.direction.y))
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
            while (battleManager.grid[randomPos.x, randomPos.y] != null)
            {
                randomPos = new Vector2Int(Random.Range(0, gridSize), Random.Range(0, gridSize));
            }
            GameObject tile = GetTileAt(randomPos.x, randomPos.y);
            if (tile != null)
            {
                GameObject enemy = Instantiate(stageSetting.enemyPrefabs[Random.Range(0, stageSetting.enemyPrefabs.Count)], transform);
                EnemyBehavior enemyBehavior = enemy.GetComponent<EnemyBehavior>();
                enemy.transform.SetPositionAndRotation(
                    CorrectOffsetPosition(tile.transform.position, 1f),
                    Quaternion.LookRotation(RotationCorrection(new Vector3(enemyBehavior.direction.x, 0, enemyBehavior.direction.y)))
                );
                enemy.transform.SetParent(transform.GetChild(0));
                enemy.name = $"Enemy_{randomPos.x}_{randomPos.y}";
                enemyBehavior.gridPosition = randomPos;
                battleManager.grid[randomPos.x, randomPos.y] = enemy;
                battleManager.entities.Add(enemyBehavior);
                enemyBehavior.battleManager = battleManager; // Set the BattleManager reference
                enemyBehavior.NextTurn += battleManager.NextTurn; // Subscribe to the NextTurn event
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