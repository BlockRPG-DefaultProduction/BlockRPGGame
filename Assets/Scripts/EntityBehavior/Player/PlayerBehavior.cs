using UnityEditor;
using UnityEngine;
public class PlayerBehavior : EntityBehavior
{
    public GameObject playerUI;
    public PlayerTileActionManager tileActionManager;

    void Awake()
    {
        tileActionManager.exectionFinished += CompleteTurn; // Subscribe to the event to complete the turn when actions are finished
    }

    public override void StartTurn()
    {
        base.StartTurn();
        tileActionManager.canBeAdded = true; // Allow adding tiles for this turn
        foreach (Transform child in playerUI.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    public override void CompleteTurn()
    {
        foreach (Transform child in playerUI.transform)
        {
            if (!tileActionManager.tileToExecute.Contains(child.gameObject) || child.GetComponent<PlacementBarBehavior>() != null)
            {
                // If the child is not in the tileToExecute list or is not a PlacementBarBehavior, hide it
                child.gameObject.SetActive(false);
            }
        }
        tileActionManager.ClearTile(true);
        tileActionManager.canBeAdded = false; // Reset the ability to add tiles for the next turn
        base.CompleteTurn();
    }
}