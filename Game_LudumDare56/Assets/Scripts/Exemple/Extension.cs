using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension_Rect
{
    public static Vector2 RandomPointInRect(this Rect rect)
    {
        return new Vector2(
            Random.Range(rect.xMin, rect.xMax),
            Random.Range(rect.yMin, rect.yMax)
            );
    }
    public static Vector3 RandomPointInRect3D(this Rect rect) => (Vector3)RandomPointInRect(rect);
}


