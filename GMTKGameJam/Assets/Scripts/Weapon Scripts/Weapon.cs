using Entities;
using SODefinitions;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using DragoRyu.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

public enum Owner
{
    ENEMY,
    PLAYER,
    BOSS
}

public abstract class Weapon : MonoBehaviour
{
    public float DamageMultiplier = 1f;
    
    public Action<Weapon> OnDecay;
    
    [FormerlySerializedAs("muzzle")] public Transform Muzzle;
    [FormerlySerializedAs("stats")] public WeaponSO Stats;
    [FormerlySerializedAs("bulletStats")] public BulletSO BulletStats;
    [FormerlySerializedAs("pickBox")] public CircleCollider2D PickBox;
    [FormerlySerializedAs("timeBetweenShots")] public float TimeBetweenShots;
    [FormerlySerializedAs("durability")] public float Durability;
    
    [FormerlySerializedAs("pickable")] public bool Pickable;

    protected Owner Owner;
    protected bool Firing;
    
    private bool _startDecay;
    
    public virtual void StartFiring()
    {
        Firing = true;
    }

    public abstract void StopFiring();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            AdaptiveFighterClass fighter = collision.GetComponent<AdaptiveFighterClass>();
            fighter.AddWeaponAvailable(this);
        }
    }

    protected void Decay()
    {
        if (!_startDecay) return;

        Durability --;
        
        if (Durability > 0) return;
        
        OnDecay.SafeInvoke(this);
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            Debug.Log("Removed");
            AdaptiveFighterClass fighter = collision.GetComponent<AdaptiveFighterClass>();
            fighter.RemoveWeaponAvailable(this);
        }
    }

    public void OnPickup(Owner pickedUpBy, float damageMultiplier, float Durability)
    {
        _startDecay = true;
        Owner = pickedUpBy;
        Pickable = false;
        PickBox.enabled = false;
        DamageMultiplier = damageMultiplier;
        this.Durability = (int)Durability;
        GameManager._pickableWeapons.Remove(this);
    }

}
