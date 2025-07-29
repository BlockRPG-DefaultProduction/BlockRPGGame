using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageSetting", menuName = "Scriptable Objects/StageSetting")]
public class StageSetting : ScriptableObject
{
    public GameObject groundTilePrefab;             // Prefab mặt đất
    public GameObject alternativeGroundTilePrefab; // Prefab mặt đất thay thế
    public List<GameObject> enemyPrefabs;             // Prefab kẻ thù

    public float tileSpacing = 1.1f;     // Khoảng cách giữa các tile
    public float timeToGenerate = 0.01f; // Thời gian tạo tile
}
