using System;
using UnityEngine;
// ReSharper disable UnusedMember.Local
// ReSharper disable InconsistentNaming

public static class PlayerPrefsManager
{
    public static EntityLevel Player = new EntityLevel("Player");
    public static EntityLevel Assault = new EntityLevel("Assault");
    public static EntityLevel DMR = new EntityLevel("DMR");
    public static EntityLevel SMG = new EntityLevel("SMG");

    public static EntityLevel GetWeaponEntity(Type weapon)
    {
        if (weapon == typeof(DMR))
        {
            return DMR;
        }
        if (weapon == typeof(SMG))
        {
            return SMG;
        }

        return Assault;
    }

    private const string Coins = "Coins";

    public static int GetCoins()
    {
        return PlayerPrefs.HasKey(Coins) ? PlayerPrefs.GetInt(Coins) : 0;
    }

    public static void IncreaseCoins(int amount)
    {
        PlayerPrefs.SetInt(Coins, GetCoins() + amount);
    }
    public static void DecreaseCoins(int amount)
    {
        PlayerPrefs.SetInt(Coins, GetCoins()-amount);
    }

}

public class EntityLevel
{
    private const string Health = "Health";
    private const string Damage = "Damage";
    private readonly string damage;
    private readonly string durability;
    public EntityLevel(string entityName)
    {
        damage = entityName + "." + Damage;
        durability = entityName + "." + Health;
    }

    public int GetDurabilityLevel()
    {
        return PlayerPrefs.HasKey(durability) ? PlayerPrefs.GetInt(durability):0;
    }

    public void UpgradeDurability()
    {
        PlayerPrefs.SetInt(durability, GetDurabilityLevel()+1);
    }

    public void DowngradeDurability()
    {
        PlayerPrefs.SetInt(durability, GetDurabilityLevel()-1);
    }

    public int GetDamageLevel()
    {
        return PlayerPrefs.HasKey(damage) ? PlayerPrefs.GetInt(damage):0;
    }

    public void UpgradeDamage()
    {
        PlayerPrefs.SetInt(damage, GetDamageLevel()+1);
    }

    public void DowngradeDamage()
    {
        PlayerPrefs.SetInt(damage, GetDamageLevel()-1);
    }
}