using System.Collections.Generic;
using UnityEngine;

namespace SODefinitions
{
    [CreateAssetMenu(fileName = "BulletType", menuName = "BulletSO")]
    // ReSharper disable once InconsistentNaming
    public class BulletSO : ScriptableObject
    {
        public float BaseDamage;
        public float BaseSpeed;
        public List<Sprite> WeaponSpecific;
    }
}
