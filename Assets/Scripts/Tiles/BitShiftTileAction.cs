using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class BitShiftTileAction : AbstractTileAction
{
    public int attackPower = 1; // Sức mạnh tấn công
    public override void Invoke()
    {
        base.Invoke();
    }
    public override IEnumerator Action()
    {
        // First attack piece
        yield return null;
        Complete();
    }
}
