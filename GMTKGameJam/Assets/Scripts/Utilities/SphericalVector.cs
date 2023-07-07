using System;
using UnityEngine;

namespace DragoRyu.Utilities
{
    public struct SphericalVector : IEquatable<SphericalVector>, IEquatable<Vector3>, IFormattable
    {
        public float Rad;
        public float Theta;
        public float Phi;
        private readonly double _tolerance;

        public SphericalVector(float rad, float theta, float phi)
        {
            this.Rad = rad;
            this.Theta = theta;
            this.Phi = phi;
            _tolerance = 0.01;
        }
        public SphericalVector(Vector3 value)
        {
            this.Rad = value.x;
            this.Theta = value.y;
            this.Phi = value.z;
            _tolerance = 0.01;
        }
        public bool Equals(SphericalVector other)
        {
            return !(Math.Abs(this.Rad - other.Rad) > _tolerance || Math.Abs(this.Theta - other.Theta) > _tolerance || Math.Abs(this.Phi - other.Phi) > _tolerance);
        }

        public bool Equals(Vector3 other)
        {
            return !(Math.Abs(this.Rad - other.x) > _tolerance || Math.Abs(this.Theta - other.y) > _tolerance || Math.Abs(this.Phi - other.z) > _tolerance);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"({Rad:#.##}, {Theta:#.##}, {Phi:#.##})";
        }
    }
}
