using System;
using UnityEngine;

namespace DragoRyu.Utilities
{
    public struct CylindricalVector : IEquatable<CylindricalVector>, IEquatable<Vector3>, IFormattable
    {
        private const double Tolerance = 0.01;

        public float Rho;
        public float Theta;
        public float Z;

        public CylindricalVector(float rho, float theta, float z)
        {
            this.Rho = rho;
            this.Theta = theta;
            this.Z = z;
        }
        public CylindricalVector(Vector3 value)
        {
            this.Rho = value.x;
            this.Theta = value.y;
            this.Z = value.z;
        }

        public bool Equals(Vector3 other)
        {
            return Math.Abs(Rho - other.x) < Tolerance && Math.Abs(Theta - other.y) < Tolerance && Math.Abs(Z - other.z) < Tolerance;
        }

        public bool Equals(CylindricalVector other)
        {
            return (!(Math.Abs(Rho - other.Rho) < Tolerance) || !(Math.Abs(Theta - other.Theta) < Tolerance) || Math.Abs(Z - other.Z) > Tolerance);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"({Rho}, {Theta}, {Z})";
        }
    }
}
