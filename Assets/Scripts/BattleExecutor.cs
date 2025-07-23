using System;
using System.Collections;
using UnityEngine;

public class BattleExecutor : MonoBehaviour
{
    public TileActionManager tileActionManager; // Quản lý trạng thái trận đấu, chứa danh sách hành động
    public PlayerBehavior player;                 // Tham chiếu đến Player


    // Coroutine thực thi các hành động theo thứ tự
    [Obsolete("Rather than magic buttons, execute action in the BattleStateManager.")]
    // private IEnumerator ExecuteActionsCoroutine()
    // {
    //     // Kiểm tra null để tránh lỗi
    //     if (battleStateManager == null || battleStateManager.tileExecute == null)
    //     {
    //         Debug.LogError("battleStateManager hoặc tileExecute chưa được gán!");
    //         yield break;
    //     }
    //     if (player == null)
    //     {
    //         Debug.LogError("Player chưa được gán!");
    //         yield break;
    //     }

    //     foreach (var action in battleStateManager.tileExecute)
    //     {
    //         switch (action)
    //         {
    //             case BattleActionList.BattleActionType.Move:
    //                 player.Move();
    //                 yield return new WaitUntil(() => !player.isExecuting); // Đợi player di chuyển xong
    //                 break;
    //             case BattleActionList.BattleActionType.RotateLeft:
    //                 player.Rotate(true);
    //                 yield return new WaitUntil(() => !player.isExecuting); // Đợi player xoay xong
    //                 break;
    //             case BattleActionList.BattleActionType.RotateRight:
    //                 player.Rotate(false);
    //                 yield return new WaitUntil(() => !player.isExecuting); // Đợi player xoay xong
    //                 break;
    //             default:
    //                 Debug.LogWarning("Unknown action type: " + action);
    //                 break;
    //         }
    //         yield return new WaitForSeconds(0.5f); // Nghỉ giữa các hành động

    //     }
    //     battleStateManager.tileExecute.Clear();
    //     PlacementBarBehavior placementBar = FindFirstObjectByType<PlacementBarBehavior>();
    //     if (placementBar != null)
    //     {
    //         placementBar.SyncWithBattleStateManager();
    //     }
    //     Debug.Log("Đã xoá toàn bộ tile sau khi thực hiện xong hành động.");
    // }

    // Hàm gọi để bắt đầu thực thi các hành động
    public void ExecuteAction()
    {
        tileActionManager.ExecuteAction();
    }
}