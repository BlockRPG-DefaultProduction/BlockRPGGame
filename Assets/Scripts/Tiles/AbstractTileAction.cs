using System;
using System.Collections;
using UnityEngine;

public abstract class AbstractTileAction : MonoBehaviour
{
    public bool isExecuting = false; // Biến để kiểm tra xem hành động có đang thực thi hay không
    public bool canConstantlyExecute = false; // Biến để kiểm tra xem hành động có thể thực thi liên tục hay không
    public PlayerBehavior player;
    public GameObject playerObject;

    private bool wasExecuting = false;

    void Update()
    {
        if (isExecuting)
        {
            Action();
        }
    }

    // Invoke: Action that is called when the moment tile starts executing
    // Similar to Awake() in MonoBehaviour, but for tile actions
    public virtual void Invoke()
    {
        isExecuting = true;
    }

    // Action: Action that is called when the tile action is executed
    // Similar to Update() in MonoBehaviour, but for tile actions
    public abstract void Action();

    // Complete: Action that is called when the tile action is completed
    // Similar to OnDestroy() in MonoBehaviour, but for tile actions
    public virtual void Complete()
    {
        isExecuting = false;
    }
}
