using System.Collections.Generic;
using UnityEngine;

namespace SODefinitions
{
    [CreateAssetMenu(fileName = "BulletType", menuName = "BulletSO")]
    public class BulletSo : ScriptableObject
    {
        public float BaseDamage;
        public float BaseSpeed;
        public List<Sprite> WeaponSpecific;
    }
}
