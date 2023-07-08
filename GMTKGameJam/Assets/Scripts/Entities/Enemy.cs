using DragoRyu.DevTools;
using DragoRyu.Utilities;
using Interfaces;
using SODefinitions;
using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

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
    [SerializeField] private float rotSpeed;
    [SerializeField] private bool alive;

    private Trigger outsideRangeTrigger;
    private Trigger fireTrigger;

    public Transform playerTransform;

    public bool pickable;
    private bool firing;
    public bool Alive { get => alive; 
        set
        {
            alive = value;
            if(!alive)
            {
                pickable = true;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        health = stats.BaseHealth;
        Alive = true;
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
        Alive = false;
    }
    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public void TakeHeal(float health)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.ExitDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stats.EngagementDistance);
    }

}
