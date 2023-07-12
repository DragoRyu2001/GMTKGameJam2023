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
    public float FireRateMultiplier = 1f;
    
    public Action<Weapon> OnDecay;
    public Action OnPickupEvent;
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
    private HealthMatManager healthManager;
    private bool lerpComplete;
    private float baseDurability;
    public float damageMultiplier=1f;
    public GameObject lightHolder;
    private void Awake()
    {
        damageMultiplier = 1f;   
    }
    public virtual void StartFiring()
    {
        Firing = true;
    }

    public abstract void StopFiring();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TryGetComponent<Enemy>(out _)) return;
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
        if (lerpComplete)
            healthManager.UpdateHealthShader(Durability / baseDurability);
        
        if (Durability > 0) return;
        
        OnDecay.SafeInvoke(this);
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            AdaptiveFighterClass fighter = collision.GetComponent<AdaptiveFighterClass>();
            fighter.RemoveWeaponAvailable(this);
        }
    }

    public void OnPickup(Owner pickedUpBy, float fireRateMultiplier, float Durability, float damageMultiplier)
    {
        StartCoroutine(RefillHealth());
        lightHolder.SetActive(true);
        _startDecay = true;
        Owner = pickedUpBy;
        Pickable = false;
        PickBox.enabled = false;
        FireRateMultiplier = fireRateMultiplier;
        baseDurability = this.Durability = (int)Durability;
        this.damageMultiplier = damageMultiplier;
        GameManager.PickableWeapons.Remove(this);
        OnPickupEvent.SafeInvoke();
    }

    private IEnumerator RefillHealth()
    {
        healthManager = GetComponent<HealthMatManager>();
        float elapsedTime = 0f;
        while (elapsedTime < (Durability / baseDurability))
        {
            yield return null;
            elapsedTime+=Time.deltaTime/2f;
            healthManager.UpdateHealthShader(elapsedTime);
        }
        lerpComplete = true;
    }
}
