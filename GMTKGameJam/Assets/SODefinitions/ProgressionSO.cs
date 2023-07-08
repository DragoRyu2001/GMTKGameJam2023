using System.Collections.Generic;
using UnityEngine;

namespace SODefinitions
{
    [CreateAssetMenu(fileName = "ProgressionSystem", menuName = "WeaponProgressionSO")]
    // ReSharper disable once InconsistentNaming
    public class ProgressionSO : ScriptableObject
    {
        public List<float> DamageProgression;
        public List<float> DurabilityProgression;
    }
}
