using SODefinitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAttribute
{
    NONE,
    HOMING,
    EXPLOSIVE,
    BLEEDING,
    TOXIC
}
public abstract class Weapon : MonoBehaviour
{
    public WeaponSO stats;

    public abstract void StartFiring();
    public abstract void StopFiring();
}
