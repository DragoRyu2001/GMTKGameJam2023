using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DragoRyu.Utilities
{
    public static class MathUtilities
    {
        
        public static float ChangeRange(NumberRange originalRange, NumberRange newRange, float originalValue)
        {
            if (originalRange.RangeZero() || newRange.RangeZero()) return newRange.Min;
            
            var slope = newRange.GetLength() / originalRange.GetLength();
            var constant = (newRange.Min - (slope * originalRange.Min));
            
            return (slope * originalValue) + constant;
        }
        public static float ChangeRange(float min1, float max1, float min2, float max2, float originalValue)
        {
            var originalRange = new NumberRange(min1, max1);
            var newRange = new NumberRange(min2, max2);
            return ChangeRange(originalRange, newRange, originalValue);
        }
        public static float SetPrecision(this float value, int digits)
        {
            var multiplier = Mathf.Pow(10, digits);
            float newValue = (int)(value * multiplier);
            newValue /= multiplier;
            return newValue;
        }

        public static T GetWeightedRandom<T>(this List<T> list, int[] weights)
        {
            var weightSum = weights.Sum();
            var value = Random.Range(0, weightSum);
            var total = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                total += weights[i];
                if (value < total)
                {
                    return list[i];
                }
            }
            return default;
        }
    }
}
