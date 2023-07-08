using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DragoRyu.Utilities
{
    [System.Serializable]
    public class WeightedRandom<T>
    {
        [SerializeField] private List<T> list;
        [SerializeField] private List<int> weights;

        public T GetWeightedRandom()
        {
            if (weights.Count != list.Count)
            {
                Debug.LogError("Number of weights does not match number of Elements in the list");
                return default;
            }
            var weightSum = weights.Sum();
            var value = Random.Range(0, weightSum);
            var total = 0;
            for (int i = 0; i < weights.Count; i++)
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