using System.Collections.Generic;
using UnityEngine;

namespace SODefinitions
{
    [CreateAssetMenu(fileName = "ProgressionSystem", menuName = "ProgressionSO")]
    // ReSharper disable once InconsistentNaming
    public class ProgressionSO : ScriptableObject
    {
        public List<float> DamageProgression;
        public List<int>   DamageCost;
        public List<float> DurabilityProgression;
        public List<int>   DurabilityCost;
    }
}
