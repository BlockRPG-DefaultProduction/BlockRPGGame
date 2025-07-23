using UnityEngine;

[CreateAssetMenu(fileName = "BattleActionList", menuName = "Scriptable Objects/BattleActionList")]
public class BattleActionList : ScriptableObject
{
    public enum BattleActionType
    {
        Move,
        RotateLeft,
        RotateRight,
    }
}
