using System;
using SODefinitions;
using UnityEngine;
// ReSharper disable InconsistentNaming

/// <summary>
/// The Player Class asks this class for its Damage Multiplier and other Details
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private ProgressionSO PlayerProgression;
    [SerializeField] private ProgressionSO AssaultProgression;
    [SerializeField] private ProgressionSO DMRProgression;
    [SerializeField] private ProgressionSO SMGProgression;

    public float GetPlayerHealth()
    {
        //TODO:- @Sanchith Fetch Level from Level Storage
        var level = 0;
        return PlayerProgression.DurabilityProgression[level];
    }

    public float GetDamageMultiplier(Weapon weapon)
    {
        var progression = GetWeaponProgression(weapon.GetType());
        //TODO:- @Sanchith Fetch Level from Level Storage
        var level = 1;
        return progression.DamageProgression[level];
    }

    public float GetDurability(Weapon weapon)
    {
        var progression = GetWeaponProgression(weapon.GetType());
        var level = 2;
        return progression.DurabilityProgression[level];
    }
    private ProgressionSO GetWeaponProgression(Type weaponType)
    {
        if (weaponType == typeof(Assault))
        {
            return AssaultProgression;
        }
        if (weaponType == typeof(DMR))
        {
            return DMRProgression;
        }
        if (weaponType == typeof(SMG))
        {
            return SMGProgression;
        }
        return PlayerProgression;
    }

}