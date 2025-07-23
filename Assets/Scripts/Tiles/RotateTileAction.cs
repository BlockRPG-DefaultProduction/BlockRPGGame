using System.Collections;
using UnityEngine;

public class RotateTileAction : AbstractTileAction
{
    // Counter clockwise = Rotate left
    // Clockwise = Rotate right
    public bool clockWise = false;
    public override IEnumerator Action()
    {
        // Currently hacky solution to execute in player behavior, i wish i have a state machine
        player.Rotate(clockWise);
        yield return new WaitUntil(() => !player.isExecuting);
        Complete();
    }
}
