using UnityEngine;

[System.Serializable]
public class TileSet
{
    public string setName;
    public GameObject[] TileList;
}

[CreateAssetMenu(fileName = "StartTile", menuName = "Scriptable Objects/StartTile")]
public class StartTile : ScriptableObject
{
    public TileSet[] tileSets;
}
