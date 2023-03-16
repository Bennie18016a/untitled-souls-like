using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossStateMachine : StateMachine
{
    #region Scripts
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public ForceReciver ForceReciver { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }
    #endregion

    #region Variables
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float Knockback { get; private set; }
    [field: SerializeField] public float MaxAttackAttemptTime { get; private set; }
    [field: SerializeField] public bool Active { get; private set; }
    #region Distances
    [field: SerializeField] public float StrafeDistance { get; private set; }
    [field: SerializeField] public float PunchDistance { get; private set; }
    [field: SerializeField] public float KickDistance { get; private set; }
    [field: SerializeField] public float ThrowDistance { get; private set; }
    #endregion

    public enum Boss { Goblin_King }
    public Boss thisBoss;
    public GameObject Player { get; private set; }
    #endregion

    #region Unity Functions
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        NavMeshAgent.updatePosition = false;
        NavMeshAgent.updateRotation = false;

        SwitchState(new BossChaseState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }
    #endregion

    #region Events
    private void HandleTakeDamage()
    {
        //SwitchState(new EnemyImpactState(this));
    }
    private void HandleDie()
    {
        GameObject.Destroy(gameObject);
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, StrafeDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, PunchDistance);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, KickDistance);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, ThrowDistance);
    }
}
