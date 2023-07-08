using Entities;
using SODefinitions;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using DragoRyu.Utilities;
using UnityEngine;

public enum WeaponAttribute
{
    NONE,
    HOMING,
    EXPLOSIVE,
    BLEEDING,
    TOXIC
}

public enum Owner
{
    ENEMY,
    PLAYER,
    BOSS
}

public abstract class Weapon : MonoBehaviour
{
    public Transform muzzle;
    public WeaponSO stats;
    public BulletSO bulletStats;
    public CircleCollider2D pickBox;
    protected Owner owner; 
    public int durability;
    private bool startDecay;
    public Action<Weapon> onDecay;
    public bool pickable;

    public virtual void StartFiring()
    {
        if(startDecay)
        {
            durability--;
            if(durability <= 0 )
            {
                onDecay.SafeInvoke(this);
                Destroy(gameObject);
            }
        }
    }
    public abstract void StopFiring();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            AdaptiveFighterClass fighter = collision.GetComponent<AdaptiveFighterClass>();
            fighter.AddWeaponAvailable(this);
            Debug.Log("Added");
        }
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

    public void OnPickup(Owner owner)
    {
        startDecay = true;
        this.owner = owner;
        pickable = false;
        pickBox.enabled = false;
    }

}
