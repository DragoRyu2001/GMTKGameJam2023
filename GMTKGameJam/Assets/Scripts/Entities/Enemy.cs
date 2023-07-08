using DragoRyu.DevTools;
using DragoRyu.Utilities;
using Entities;
using Interfaces;
using SODefinitions;
using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

public enum EnemyStates
{
    CHASE,
    ATTACK
}
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyStates enemyState;
    [SerializeField] private Weapon weapon;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private CharacterSO stats;
    [SerializeField] private Collider2D hitbox;
    [SerializeField] private Collider2D pickBox;

    [SerializeField] private float health;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private bool alive;

    private Trigger outsideRangeTrigger;
    private Trigger fireTrigger;

    public Transform playerTransform;
    public bool Alive { get => alive; 
        set
        {
            alive = value;
            if(!alive)
            {
                weapon.StopFiring();
                weapon.Pickable = true;
                pickBox.enabled = true;
                hitbox.enabled = false;
                gameObject.layer = LayerMask.NameToLayer("Pickable");
                movement.KillMovement();
                Destroy(this);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        health = stats.BaseHealth;
        Alive = true;
        playerTransform = GameManager.Instance.PlayerTransform;
        outsideRangeTrigger = new Trigger(() => { StartAttack(); });
        fireTrigger = new Trigger(weapon.StopFiring, weapon.StartFiring);
        movement.Init(stats.MoveSpeed, playerTransform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Alive)
        {
            TrackDistance();   
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            Kill();
        }
    }

    private void TrackDistance()
    {
        if (playerTransform == null) return;
        distanceToPlayer = (transform.position - playerTransform.position).magnitude;
        if (distanceToPlayer > stats.ExitDistance)
        {
            outsideRangeTrigger.ResetTrigger();
            if (distanceToPlayer > stats.EngagementDistance)
            {
                GiveChase();
            }
        }
        else if (distanceToPlayer < stats.ExitDistance)
        {
            if (distanceToPlayer < stats.EngagementDistance)
            {
                 outsideRangeTrigger.SetTrigger();
            }
        }
    }
    private void GiveChase()
    {
        enemyState = EnemyStates.CHASE;
        movement.StartMove();
        fireTrigger.SetTrigger();
    }

    private void StartAttack()
    {
        enemyState |= EnemyStates.ATTACK;
        movement.EndMove();
        fireTrigger.ResetTrigger();
    }

    #region IDamageable stuff
    public void Kill()
    {
        GameManager.Instance.EnemyDied(this);
        float dice = Random.Range(0, 1f);
        if (dice < GameManager.Instance.DropChance)
        {
            Alive = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        Kill();
    }

    public void TakeHeal(float health)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
