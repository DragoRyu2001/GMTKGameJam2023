using UnityEngine;

namespace SODefinitions
{
    [CreateAssetMenu(fileName = "WeaponType", menuName = "WeaponSO")]
    public class WeaponSO : ScriptableObject
    {
        public float FireRate; 
        public float BulletSpeedMultiplier;
        public float WeaponDamage;
    }
}
