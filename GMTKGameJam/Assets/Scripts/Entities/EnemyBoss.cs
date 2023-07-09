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
        if(GameManager._pickableWeapons.Count!=0)
        {
            Debug.Log("Called again");
            int limit = Mathf.Min(WeaponAimSystem.WeaponCapacity - WeaponAimSystem.WeaponCount, GameManager._pickableWeapons.Count - 1);
            Debug.Log("Limit"+limit+GameManager._pickableWeapons.Count);
            Weapon w = GameManager._pickableWeapons[0];
            WeaponAimSystem.AddWeapon(w);
            w.OnPickup(Owner.BOSS, CharacterSo.CharDamageMultiplier, PlayerStats.Instance.GetBaseDurability(w));
            GameManager._pickableWeapons.Remove(w);

            SetState();
            inAction = false;
            yield break;
        }

        Collider2D enemyColl = Physics2D.OverlapCircle(transform.position, 20f, LayerMask.NameToLayer("Enemy"));
        if(enemyColl == null)
        {
            target = GameManager._activeEnemies[UnityEngine.Random.Range(0, GameManager._activeEnemies.Count - 1)].transform;
            Debug.Log("Picking Random");
        }
        else
        {
            target = enemyColl.transform;
            Debug.Log("Got Enemy");
        }
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
        while(elapsedTime<=ScavengeTime)
        {
            if(!enemy.Alive)
            {
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
