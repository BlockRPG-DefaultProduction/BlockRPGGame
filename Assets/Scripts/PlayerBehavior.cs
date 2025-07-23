using System;
using UnityEngine;
using static Utilities.VectorHelper;

public class PlayerBehavior : MonoBehaviour
{
    public float velocity = 5f;
    public float rotationSpeed = 180f; // Độ xoay mỗi giây
    public Vector2Int gridPosition = new Vector2Int(0, 0); // (row, col) trong grid
    public Vector2Int direction = Vector2Int.up; // Hướng mặc định: tiến lên (Y+)
    public Animator playerAnimation;
    private MapManager map;

    void Start()
    {
        map = FindFirstObjectByType<MapManager>();

        playerAnimation = GetComponent<Animator>();
        if (playerAnimation == null)
        {
            Debug.LogError("PlayerAnimation component not found!");
        }
    }
}