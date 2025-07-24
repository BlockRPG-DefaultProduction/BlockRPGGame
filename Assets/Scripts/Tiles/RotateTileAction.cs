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
        executionTime = 90 / entity.rotationSpeed;
        entity.direction = Rotation2i(entity.direction, rotateAngle);
    }
    public override void Action()
    {
        float elapsedTime = Time.time - startTime;
        float t = elapsedTime / executionTime;
        entity.transform.Rotate(Vector3.up * entity.rotationSpeed * Time.deltaTime * (clockWise ? -1 : 1));
        if (t >= 1f)
        {
            entity.transform.rotation = Quaternion.LookRotation(RotationCorrection(new Vector3(entity.direction.x, 0, entity.direction.y)));
            Complete();
            Debug.Log("Finished rotating to direction: " + entity.direction);
        }
    }

}
