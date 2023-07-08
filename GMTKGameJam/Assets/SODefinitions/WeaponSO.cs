using UnityEngine;

namespace SODefinitions
{
    [CreateAssetMenu(fileName = "WeaponType", menuName = "WeaponSO")]
    public class WeaponSO : ScriptableObject
    {
        public float FireRate;  //bullets per second?
        public float BulletSpeedMultiplier;
        public float WeaponDamageMultiplier;
        public WeaponAttribute WeaponAttribute; 
    }
}
