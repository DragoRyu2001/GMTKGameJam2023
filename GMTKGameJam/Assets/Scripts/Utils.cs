using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DragoRyu.Utilities;

public static class Utils 
{
    public static Vector2 Rotate2D(this Vector2 v, float angle)
    {
        Vector2 rotVec = new Vector2();
        float s  = Mathf.Sin(angle);
        float c = Mathf.Cos(angle);
        rotVec.x = c * v.x - s*v.y;
        rotVec.y = s * v.x + c*v.y;
        return rotVec;
    }
}
