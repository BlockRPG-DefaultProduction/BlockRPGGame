using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utilities.VectorHelper;
public class MapManager : MonoBehaviour
{
    public GameObject groundTilePrefab;             // Prefab mặt đất
    public GameObject alternativeGroundTilePrefab; // Prefab mặt đất thay thế
    public int gridSize = 5;             // Kích thước lưới, ví dụ 4 hoặc 5
    public float tileSpacing = 1.1f;     // Khoảng cách giữa các tile

    public float timeToGenerate = 0.05f; // Thời gian tạo tile

    private List<GameObject> groundTiles = new List<GameObject>();
    private PlayerBehavior player;        // Tham chiếu đến Player

    void Start()
    {
        player = FindFirstObjectByType<PlayerBehavior>();
        StartCoroutine(GenerateGrid());
        PlacePlayer(); // Đặt player sau khi sinh xong map
    }

    private IEnumerator GenerateGrid()
    {
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                Vector3 position = new Vector3(col * tileSpacing, 0, row * tileSpacing);
                InstantiateTile(position, row, col);
                yield return new WaitForSeconds(timeToGenerate);
            }
        }
        
    }

    // Little Instantiate Animation
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
        groundTiles.Add(tile);
    }

    // Đặt Player lên tile ban đầu
    void PlacePlayer()
    {
        if (player == null) return;
        GameObject startTile = GetTileAt(player.gridPosition.x, player.gridPosition.y);
        if (startTile != null)
        {
            player.SetPlayerPositionAndRotation(
                CorrectOffsetPosition(startTile.transform.position, 1f),
                Quaternion.LookRotation(RotationCorrection(new Vector3(player.direction.x, 0, player.direction.y)))
            );
        }
        else
        {
            Debug.LogError("Start tile does not exist. Check gridPosition.");
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