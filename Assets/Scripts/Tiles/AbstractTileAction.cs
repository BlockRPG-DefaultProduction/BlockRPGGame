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
        if (isExecuting && (!wasExecuting || canConstantlyExecute))
        {
            StartCoroutine(Action());
        } 
        wasExecuting = isExecuting;
    }

    public virtual void Invoke()
    {
        isExecuting = true;
    }

    public abstract IEnumerator Action();
    public virtual void Complete()
    {
        isExecuting = false;
    }
}
