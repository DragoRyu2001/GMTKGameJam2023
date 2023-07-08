using System;
using UnityEngine;

namespace DragoRyu.Utilities
{
    public struct NumberRange: IEquatable<NumberRange>, IFormattable
    {
        public float Min;
        public float Max;
        private readonly double _tolerance;

        public NumberRange(float min, float max)
        {
            Min = min;
            Max = max;
            _tolerance = 0.01;
        }
        public bool RangeZero()
        {
            return Max - Min == 0;
        }
        public float GetLength()
        {
            return Mathf.Abs(Max - Min);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"({Min:#.##}-{Max:#.##})";
        }

        public bool Equals(NumberRange other)
        {
            return (Math.Abs(Min - other.Min) < _tolerance && Math.Abs(Max - other.Max) < _tolerance);
        }
    }
}
