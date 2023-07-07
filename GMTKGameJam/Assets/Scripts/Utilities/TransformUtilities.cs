using UnityEngine;

namespace DragoRyu.Utilities
{
    public static class TransformUtilities
    {
        public static SphericalVector GetSphericalCoordinates(this Vector3 cartesianPosition, Vector3 origin = default)
        {
            cartesianPosition -= origin;

            SphericalVector sphericalPosition = default;
            sphericalPosition.Rad   = Mathf.Sqrt(Vector3.Dot(cartesianPosition, cartesianPosition));
            sphericalPosition.Theta = Mathf.Atan2(cartesianPosition.y, cartesianPosition.x) * Mathf.Rad2Deg;
            sphericalPosition.Phi   = Mathf.Atan2(Mathf.Sqrt(Vector2.Dot(cartesianPosition.XY(), cartesianPosition.XY())), cartesianPosition.z) * Mathf.Rad2Deg;

            return sphericalPosition;
        }

        public static Vector3 GetCartesianCoordinates(this SphericalVector sphericalVector, Vector3 origin = default)
        {
            Vector3 cartesianPosition;
            var phi = sphericalVector.Phi * Mathf.Deg2Rad;
            var theta = sphericalVector.Theta * Mathf.Deg2Rad;
            cartesianPosition.x = sphericalVector.Rad * Mathf.Sin(phi) * Mathf.Cos(theta);
            cartesianPosition.y = sphericalVector.Rad * Mathf.Sin(phi) * Mathf.Sin(theta);
            cartesianPosition.z = sphericalVector.Rad * Mathf.Cos(phi);
            cartesianPosition += origin;
            
            return cartesianPosition;
        }

        public static CylindricalVector GetCylindricalCoordinates(this Vector3 cartesianPosition, Vector3 origin = default)
        {
            cartesianPosition -= origin;

            CylindricalVector cylindricalVector;
            cylindricalVector.Rho = Mathf.Sqrt(Vector2.Dot(cartesianPosition.XY(), cartesianPosition.XY()));
            cylindricalVector.Theta = Mathf.Atan2(cartesianPosition.y, cartesianPosition.x) * Mathf.Rad2Deg;
            cylindricalVector.Z = cartesianPosition.z;

            return cylindricalVector;
        }
        public static Vector3 GetCartesianCoordinates(this CylindricalVector cylindricalVector, Vector3 origin = default)
        {
            Vector3 cartesianPosition;

            var theta = cylindricalVector.Theta * Mathf.Deg2Rad;
            cartesianPosition.x = cylindricalVector.Rho * Mathf.Cos(theta);
            cartesianPosition.y = cylindricalVector.Rho * Mathf.Sin(theta);
            cartesianPosition.z = cylindricalVector.Z;
            cartesianPosition += origin;

            return cartesianPosition;
        }

    }
}
