using System;
using System.Collections;
using UnityEngine;

public class BattleExecutor : MonoBehaviour
{
    public TileActionManager tileActionManager;
    public void ExecuteAction()
    {
        tileActionManager.ExecuteAction();
    }
}