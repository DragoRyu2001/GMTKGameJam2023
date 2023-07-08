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

    public int durability;
    public bool startDecay;

    public virtual void StartFiring()
    {
        if(startDecay)
        {
            if(durability == 0 )
            {
                //discard here
                return;
            }
            durability--;
        }
    }
    public abstract void StopFiring();
}
