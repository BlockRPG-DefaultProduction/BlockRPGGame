using UnityEngine;

[System.Serializable]
public class MovesEntry
{
    public string moveName;
    public GameObject[] tiles;
}


[CreateAssetMenu(fileName = "TilePrefabList", menuName = "Scriptable Objects/TilePrefabList")]
public class TilePrefabList : ScriptableObject
{
    // public EntityBehavior entityBehavior; // Reference to the entity behavior, can be PlayerBehavior or any other entity type
    public MovesEntry[] moves;
}
