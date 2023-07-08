using Entities;
using SODefinitions;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using DragoRyu.Utilities;
using UnityEngine;

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
    public Action<Weapon> onDecay;
    protected Owner owner;

    public float timeBetweenShots;
    public int durability;
    protected bool startDecay;
    public bool pickable;
    protected bool firing;

    public virtual void StartFiring()
    {
        firing = true;
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
        if (startDecay)
        {
            durability--;
            if (durability <= 0)
            {
                onDecay.SafeInvoke(this);
                Destroy(gameObject);
                return;
            }
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
