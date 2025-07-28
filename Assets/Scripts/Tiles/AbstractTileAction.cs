using UnityEngine;

// Do i need to not extend MonoBehaviour?
public abstract class AbstractTileAction : MonoBehaviour
{
    public bool isExecuting = false; // Biến để kiểm tra xem hành động có đang thực thi hay không
    public EntityBehavior entity;
    public BattleManager battleManager;
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
    public virtual void Action()
    {

    }
    // Complete: Action that is called when the tile action is completed
    // Similar to OnDestroy() in MonoBehaviour, but for tile actions
    public virtual void Complete()
    {
        isExecuting = false;
    }
}
