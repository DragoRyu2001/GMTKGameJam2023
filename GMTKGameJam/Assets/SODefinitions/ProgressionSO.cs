using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SODefinitions
{
    [CreateAssetMenu(fileName = "ProgressionSystem", menuName = "ProgressionSO")]
    // ReSharper disable once InconsistentNaming
    public class ProgressionSO : ScriptableObject
    {
        [FormerlySerializedAs("DamageProgression")] public List<float> FireRateProgression;
        public List<int>   DamageCost;
        public List<float> DurabilityProgression;
        public List<int>   DurabilityCost;
    }
}
