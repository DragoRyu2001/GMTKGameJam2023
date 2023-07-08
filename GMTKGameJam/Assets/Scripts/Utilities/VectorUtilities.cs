using System;
using UnityEngine;

namespace DragoRyu.Utilities
{
    public static class VectorUtilities
    {
        public static Vector3 VectorUp(this float value)
        {
            return Vector3.up * value;
        }
        public static Vector3 VectorRight(this float value)
        {
            return Vector3.right * value;
        }
        public static Vector3 VectorLeft(this float value)
        {
            return Vector3.left * value;
        }
        public static Vector3 VectorDown(this float value)
        {
            return Vector3.down * value;
        }

        #region Swizzling
        public static Vector2 XY(this Vector3 value)
        {
            return new Vector2 (value.x, value.y);
        }
        public static Vector2 XZ(this Vector3 value)
        {
            return new Vector2(value.x, value.z);
        }
        public static Vector2 YZ(this Vector3 value)
        {
            return new Vector2(value.y, value.z);
        }
        public static Vector3 XYZ(this Vector3 value)
        {
            return new Vector3(value.x, value.y, value.z);
        }

        public static Vector3 XZY(this Vector3 value)
        {
            return new Vector3(value.x, value.z, value.y);
        }

        public static Vector3 YXZ(this Vector3 value)
        {
            return new Vector3(value.y, value.x, value.z);
        }

        public static Vector3 YZX(this Vector3 value)
        {
            return new Vector3(value.y, value.z, value.x);
        }

        public static Vector3 ZXY(this Vector3 value)
        {
            return new Vector3(value.z, value.x, value.y);
        }

        public static Vector3 ZYX(this Vector3 value)
        {
            return new Vector3(value.z, value.y, value.x);
        }
        #endregion

        public static SphericalVector ToSpherical(this Vector3 value)
        {
            return new SphericalVector(value);
        }
        public static Vector3 ToVector(this SphericalVector value)
        {
            return new Vector3(value.Rad, value.Theta, value.Phi);
        }
        public static Vector3 SetVectorPrecision(this Vector3 value, int digits)
        {
            value.x = value.x.SetPrecision(digits);
            value.y = value.y.SetPrecision(digits);
            value.z = value.z.SetPrecision(digits);
            return value;
        }

        public static float ClampedMagnitude(this Vector3 value, Vector3 clampVector)
        {
            value.Scale(clampVector);
            return value.magnitude;
        }
        
    }
}
