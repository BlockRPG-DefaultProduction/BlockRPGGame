using System.Collections;
using UnityEngine;
using static Utilities.VectorHelper;

public class RotateTileAction : AbstractTileAction
{
    // Counter clockwise = Rotate left
    // Clockwise = Rotate right
    public bool clockWise = false;
    private float executionTime;
    private float startTime;
    public override void Invoke()
    {
        base.Invoke();
        int rotateAngle = clockWise ? -90 : 90;
        startTime = Time.time;
        executionTime = 90 / player.rotationSpeed;
        player.direction = Rotation2i(player.direction, rotateAngle);
    }
    public override void Action()
    {
        float elapsedTime = Time.time - startTime;
        float t = elapsedTime / executionTime;
        player.transform.Rotate(Vector3.up * player.rotationSpeed * Time.deltaTime * (clockWise ? -1 : 1));
        if (t >= 1f)
        {
            player.transform.rotation = Quaternion.LookRotation(RotationCorrection(new Vector3(player.direction.x, 0, player.direction.y)));
            Complete();
            Debug.Log("Finished rotating to direction: " + player.direction);
        }
    }

}
