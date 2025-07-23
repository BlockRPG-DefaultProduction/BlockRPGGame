using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class MoveTileAction : AbstractTileAction
{
    public override IEnumerator Action()
    {
        player.Move();
        yield return new WaitUntil(() => !player.isExecuting);
        Complete();
    }
}
