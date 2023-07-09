using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SODefinitions
{
    [CreateAssetMenu(fileName = "ProgressionSystem", menuName = "ProgressionSO")]
    // ReSharper disable once InconsistentNaming
    public class ProgressionSO : ScriptableObject
    {
        public List<float> FireRateProgression;
        public List<float> DurabilityProgression;
    }
}
