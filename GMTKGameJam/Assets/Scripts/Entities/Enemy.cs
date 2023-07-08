using DragoRyu.DevTools;
using Interfaces;
using SODefinitions;
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
    [SerializeField] private CharacterSO stats;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Collider2D hitbox;
    [SerializeField] private Collider2D pickBox;

    [SerializeField] private float health;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private float rotSpeed;
    [SerializeField] private bool alive;

    private Trigger outsideRangeTrigger;
    public Transform playerTransform;

    public bool pickable;
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
        distanceToPlayer = (transform.position - playerTransform.position).sqrMagnitude;
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
        agent.destination = playerTransform.position;
        weapon.StopFiring();
    }

    private void StartAttack()
    {
        enemyState |= EnemyStates.ATTACK;
        agent.ResetPath();
        weapon.StartFiring();
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

}
