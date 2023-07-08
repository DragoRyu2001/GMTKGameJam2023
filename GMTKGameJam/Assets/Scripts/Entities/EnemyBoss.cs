using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using Unity.VisualScripting;
using UnityEngine;


public enum BossStates 
{
    SCAVENGE,
    HUNT,
}

public class EnemyBoss : AdaptiveFighterClass
{
    public BossStates state;
    private EnemyMovement movement;
    private Transform target;
    public float[] huntChance;
    private bool inAction;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        SetState();
    }

    private void SetState()
    {
        float threshold = huntChance[WeaponAimSystem.WeaponCount-1];
        if(UnityEngine.Random.Range(0f,1f)>threshold)
        {
            state = BossStates.HUNT;
        }
        else
        {
            state = BossStates.SCAVENGE;
        }
    }

    private void Update()
    {
        if(!inAction)
            HandleStates();

    }

    private void HandleStates()
    {
        switch (state)
        {
            case BossStates.SCAVENGE:
                {
                    inAction = true;
                    StartCoroutine(Scavenge());
                }
                break;
            case BossStates.HUNT:
                break;
        }
    }

    private IEnumerator Scavenge()
    {
        yield return null;
       //target = GameManager.instance.
    }

    protected override void MovementLogic()
    {
       
    }
    //
    protected override void DamageLogic()
    {
        throw new System.NotImplementedException();
    }

    protected override void PickUpWeapon()
    {
        AvailableWeapons[0].OnPickup(Owner.BOSS, 2, 0.5f);
        throw new System.NotImplementedException();
    }

    public void SetData()
    {
        base.SetData();
    }
}
