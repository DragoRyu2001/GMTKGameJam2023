using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using DragoRyu.DevTools;

public enum BossStates 
{
    SCAVENGE,
    HUNT,
}

public class EnemyBoss : AdaptiveFighterClass
{
    public BossStates State;
    public float[] HuntChance;
    public float ScavengeTime;
    public float TotalHuntTime;

    private EnemyMovement movement;
    private Transform target;
    private Enemy targetEnemy;
    private Trigger outsideRangeTrigger;
    private Trigger insideEngagementRangeTrigger;

    private bool inAction;
    private bool inCycle;
    private bool trackDistance;

    private float distanceToTarget;

    private void Awake()
    {
        SetData();
        movement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        outsideRangeTrigger = new Trigger(GiveChase);
        insideEngagementRangeTrigger = new Trigger(Attack);
        SetState();
    }

    private void SetState()
    {
        float threshold = HuntChance[WeaponAimSystem.WeaponCount];
        if(UnityEngine.Random.Range(0f,100f)>threshold)
        {
            State = BossStates.HUNT;
        }
        else
        {
            State = BossStates.SCAVENGE;
        }
    }

    private void Update()
    {
        if(!inAction)
            HandleStates();

        if(trackDistance)
        {
            DamageLogic();
            TrackDistance();
        }
    }

    private void HandleStates()
    {
        switch (State)
        {
            case BossStates.SCAVENGE:
                {
                    inAction = true;
                    StartCoroutine(Scavenge());
                }
                break;
            case BossStates.HUNT:
                {
                    inAction = true;
                    StartCoroutine(Hunt());
                }
                break;
        }
    }

    private void TrackDistance()
    {
        if (target == null) return;
        distanceToTarget = (transform.position - target.position).magnitude;
        if (distanceToTarget > CharacterSo.ExitDistance)
        {
            insideEngagementRangeTrigger.ResetTrigger();
            if (distanceToTarget > CharacterSo.EngagementDistance)
            {
                outsideRangeTrigger.SetTrigger();
            }
        }
        else if (distanceToTarget < CharacterSo.ExitDistance)
        {
            outsideRangeTrigger.ResetTrigger();
            if (distanceToTarget < CharacterSo.EngagementDistance)
            {
                insideEngagementRangeTrigger.SetTrigger();
            }
        }
    }

    private IEnumerator Hunt()
    {
        target = GameManager.Instance.PlayerTransform;
        trackDistance = true;

        float elapsedTime = 0f;
        while(elapsedTime<=TotalHuntTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            if(WeaponAimSystem.WeaponCount<2)
            {
                trackDistance = false;
                SetState();
                inAction = false;
                yield break;
            }

        }
        SetState();
        inAction = false;
        trackDistance = false;
    }
    private void GiveChase()
    {
        movement.Init(CharacterSo.MoveSpeed ,target);
        movement.StartMove();
        WeaponAimSystem.StopFiring();
    }

    private void Attack()
    {
        movement.EndMove();
        WeaponAimSystem.StartFiring();
    }

    private IEnumerator Scavenge()
    {
        trackDistance = true;
        if(GameManager.PickableWeapons.Count!=0)
        {
            Weapon w = GameManager.PickableWeapons[^1];
            WeaponAimSystem.AddWeapon(w);
            w.OnPickup(Owner.BOSS, CharacterSo.CharDamageMultiplier, PlayerStats.Instance.GetBaseDurability(w), 0.5f);

            SetState();
            inAction = false;
            yield break;
        }

        if(GameManager.ActiveEnemies.Count==0)
        {
            yield return new WaitUntil(()=>GameManager.ActiveEnemies.Count > 0);
        }

        target = GameManager.ActiveEnemies[UnityEngine.Random.Range(0, GameManager.ActiveEnemies.Count - 1)].transform; 

        targetEnemy = target.GetComponent<Enemy>();
        trackDistance = true;
        yield return StartCoroutine(TimeoutOrEnemyDeath(targetEnemy));
        trackDistance = false;
        SetState();
        inAction = false;
    }

    private IEnumerator TimeoutOrEnemyDeath(Enemy enemy)
    {
        float elapsedTime =  0;
        while (elapsedTime <= ScavengeTime)
        {
            if (!enemy.Alive)
            {
                yield break;
            }
            if (enemy == null)
            {
                target = null;
                yield break;
            }
            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }
    

    protected override void MovementLogic()
    {
       
    }


    protected override void DamageLogic()
    {
        if(target == null) return;
        WeaponAimSystem.AimLogic(target.position);
    }

    protected override void PickUpWeapon()
    {
        
    }
}
