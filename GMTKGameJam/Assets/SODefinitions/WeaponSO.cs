using UnityEngine;

[CreateAssetMenu(fileName = "WeaponType", menuName = "WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public float fireRate;  //bullets per second?
    public float bulletSpeedMultiplier;
    public float weaponDamageMultiplier;
    public WeaponAttribute weaponAttribute; 
}
