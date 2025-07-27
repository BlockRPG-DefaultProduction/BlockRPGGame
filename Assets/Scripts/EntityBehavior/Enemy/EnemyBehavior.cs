using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBehavior : EntityBehavior
{
    public float thinkingTime = 1f; // Time to think before executing actions
    public TilePrefabList tilePrefabList; // Reference to the TilePrefabList for enemy actions
    public EnemyTileActionManager enemyTileActionManager; // Reference to the EnemyTileActionManager

    void Awake()
    {
        enemyTileActionManager.exectionFinished += CompleteTurn; // Subscribe to the event to complete the turn when actions are finished
        enemyTileActionManager.entityBehavior = this; // Set the entity behavior for the tile action manager
    }
    public override void StartTurn()
    {
        base.StartTurn();
        var randomMoves = tilePrefabList.moves[Random.Range(0, tilePrefabList.moves.Length)];
        enemyTileActionManager.tileToExecute.Clear(); // Clear previous actions
        InstantiateTile(randomMoves.tiles); // Instantiate the tiles for the enemy actions
        StartCoroutine(ExecuteEnemyActions()); // Start executing actions after a delay
    }

    void InstantiateTile(GameObject[] tiles)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            GameObject tile = tiles[i];
            GameObject instantiatedTile = Instantiate(tile);
            enemyTileActionManager.tileToExecute.Add(instantiatedTile);
        }
    }

    IEnumerator ExecuteEnemyActions()
    {
        yield return new WaitForSeconds(thinkingTime); // Wait for the thinking time before executing actions
        Debug.Log($"tileToExecute count: {enemyTileActionManager.tileToExecute.Count}");
        foreach (var t in enemyTileActionManager.tileToExecute)
        {
            Debug.Log(t);
        }
        enemyTileActionManager.ExecuteAction(); // Execute the actions
    }
    
    public override void CompleteTurn()
    {
        enemyTileActionManager.ClearActions(true); // Clear actions after completing the turn
        base.CompleteTurn();
    }
    
}