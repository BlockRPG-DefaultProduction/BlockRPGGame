using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBehavior : MonoBehaviour
{
    public PlayerBehavior playerBehavior;
    public GameObject healthTile;
    private Stack<GameObject> healthTiles = new();

    void Start()
    {
        playerBehavior.OnHealthChanged += OnHealthChanged;

        for (int i = 0; i < playerBehavior.health; i++)
        {
            GameObject tile = Instantiate(healthTile, transform);
            tile.name = "HealthTile" + i;
            tile.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-461 + i * 51, 244);
            healthTiles.Push(tile);
        }
    }

    public void OnHealthChanged(int amount, bool isHealing)
    {
        if (!isHealing)
        {
           for (int i = 0; i < amount; i++)
           {
               if (healthTiles.Count > 0)
               {
                   GameObject tile = healthTiles.Pop();
                   tile.GetComponentInChildren<Animator>().SetTrigger("HealthDrop");
                   Destroy(tile, 1f);
               }
           }
        }
    }
}
