using UnityEngine;

namespace Utilities
{
public static class VectorHelper
{
    public static Vector3 CorrectOffsetPosition(Vector3 vec, float YOffset)
    {
        return new Vector3(vec.x, vec.y + YOffset, vec.z);
    }

    public static Vector3 Rotation2(Vector3 vec, float radAngle)
    {
        return new Vector3(
            vec.x * Mathf.Cos(radAngle) - vec.z * Mathf.Sin(radAngle),
            vec.y,
            vec.x * Mathf.Sin(radAngle) + vec.z * Mathf.Cos(radAngle)
        );
    }

    public static Vector3 RotationCorrection(Vector3 vec)
    {
    /* 
     * Lesson learned: Y+ = X in Vec3, X+ = Z+ in Vec3 in Rotation. 
     * I don't really know why the fuck this happened, but it is how it is.
     * Or blame the fucking model, i guess? Kenney has made a good prototype model but the pre-rotation is pissing me off.
     * I'm the original
     *                          Starwalker
     * No Copilot, the model suck. Don't blame the coordinate system.
     * Update: Why didn't I just rotate the model? I could prevent all of this mess if I just pre-rotate.
     * Too bad this message is already aged like milk.
     */
        return Rotation2(vec, Mathf.PI / 2f); // Quay 90 độ
    }

    public static Vector2 Rotation2(Vector2 vec, float radAngle)
    {
        return new Vector2(
            vec.x * Mathf.Cos(radAngle) - vec.y * Mathf.Sin(radAngle),
            vec.x * Mathf.Sin(radAngle) + vec.y * Mathf.Cos(radAngle)
        );
    }
    public static Vector2Int Rotation2i(Vector2Int vec, float radAngle)
    {
        return new Vector2Int(
            Mathf.RoundToInt(vec.x * Mathf.Cos(radAngle) - vec.y * Mathf.Sin(radAngle)),
            Mathf.RoundToInt(vec.x * Mathf.Sin(radAngle) + vec.y * Mathf.Cos(radAngle))
        );
    }
}
}
