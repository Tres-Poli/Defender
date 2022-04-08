using UnityEngine;

public static class MathHelper
{
    public static float LerpForAtan2(float x, float y, float t)
    {
        var xAbs = Mathf.Abs(x);

        if (xAbs >= 180)
        {
            var curr = -Mathf.Sign(x) * (180 - xAbs + 180);
            return Mathf.Lerp(curr, y, t);
        }
        else if (Mathf.Abs(x - y) > 180)
        {
            var dest = -Mathf.Sign(y) * (180 - Mathf.Abs(y) + 180);
            return Mathf.Lerp(x, dest, t);
        }
        else
        {
            return Mathf.Lerp(x, y, t);
        }
    }

    public static bool IsInAttackRange(Vector3 a, Vector3 b, float attackRange, out float distance)
    {
        distance = DistanceOnPlane(a, b);
        return distance <= attackRange;
    }

    public static bool IsInAttackRange(Vector3 a, Vector3 b, float attackRange)
    {
        var distance = DistanceOnPlane(a, b);
        return distance <= attackRange;
    }

    public static float DistanceOnPlane(Vector3 a, Vector3 b)
    {
        var distance = a - b;
        distance.y = 0;
        return distance.magnitude;
    }
}
