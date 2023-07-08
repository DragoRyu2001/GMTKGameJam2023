using System.Collections.Generic;
using UnityEngine;

namespace SODefinitions
{
    [CreateAssetMenu(fileName = "BulletType", menuName = "BulletSO")]
    public class BulletSO : ScriptableObject
    {
        public float BaseDamage;
        public float BaseSpeed;
        public Bullet BulletPrefab;
        public List<Sprite> WeaponSpecific;
    }
}
