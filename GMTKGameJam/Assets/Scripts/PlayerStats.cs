﻿using System;
using SODefinitions;
using UnityEngine;
// ReSharper disable InconsistentNaming

/// <summary>
/// The Player Class asks this class for its Damage Multiplier and other Details
/// </summary>
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    [SerializeField] private ProgressionSO PlayerProgression;
    [SerializeField] private ProgressionSO AssaultProgression;
    [SerializeField] private ProgressionSO DMRProgression;
    [SerializeField] private ProgressionSO SMGProgression;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public float GetBaseDurability(Weapon weapon)
    {
        var progression = GetProgression(weapon.GetType());       
        return progression.DurabilityProgression[0];
    }

    public float GetPlayerHealth()
    {
        var level = PlayerPrefsManager.Player.GetDurabilityLevel();
        return PlayerProgression.DurabilityProgression[level];
    }

    public float GetFireRateMultiplier(Weapon weapon)
    {
        var progression = GetProgression(weapon.GetType());
        var level = PlayerPrefsManager.GetWeaponEntity(weapon.GetType()).GetFireRateLevel();
        return progression.FireRateProgression[level];
    }

    public float GetDurability(Weapon weapon)
    {
        var progression = GetProgression(weapon.GetType());
        var level = PlayerPrefsManager.GetWeaponEntity(weapon.GetType()).GetDurabilityLevel();
        return progression.DurabilityProgression[level];
    }

    public float GetDashResetTime()
    {
        return 1.5f;
    }

    public ProgressionSO GetProgression(Type weaponType = null)
    {
        if(weaponType==null)
            return PlayerProgression;
        if (weaponType == typeof(Assault))
        {
            return AssaultProgression;
        }
        if (weaponType == typeof(Sniper))
        {
            return DMRProgression;
        }
        if (weaponType == typeof(SMG))
        {
            return SMGProgression;
        }
        return null;
    }

}