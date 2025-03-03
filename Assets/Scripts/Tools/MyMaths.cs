using UnityEngine;
using System;

public class MyMaths : MonoBehaviour
{
    public static float GetAngleBetweenPoints(Vector3 center, Vector3 point)
    {
        Vector3 direction = new Vector3(point.x, 0, point.z) - new Vector3(center.x, 0, center.z);
        float angle = Mathf.Atan2(direction.z, direction.x);

        if (angle < 0) angle += 2 * Mathf.PI;

        angle *= Mathf.Rad2Deg;
        angle %= 180;
        angle = (float)Math.Round(angle, 2);

        return angle;
    }

    public static float GetAngle(Vector3 center, Vector3 object1, Vector3 object2)
    {
        Vector3 vector1 = object1 - center;
        Vector3 vector2 = object2 - center;

        float dotProduct = Vector3.Dot(vector1.normalized, vector2.normalized);

        dotProduct = Mathf.Clamp(dotProduct, -1f, 1f);
        float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;

        if (angle < 0) angle += 2 * Mathf.PI;

        return angle;
    }

    public static float IsNear(float angle1, float angle2)
    {
        var angle1_temp = Mathf.Repeat(angle1, 180f);
        var angle2_temp = Mathf.Repeat(angle2, 180f);

        float difference = Mathf.Abs(angle1_temp - angle2_temp);
        float shortestDistance = Mathf.Min(difference, 180f - difference);

        return shortestDistance;
    }

    public static float GetDistanceBetweenPoints(Vector3 point1, Vector3 point2)
    {
        return Vector3.Distance(new Vector3(point1.x, 0, point1.z), new Vector3(point2.x, 0, point2.z));
    }
}