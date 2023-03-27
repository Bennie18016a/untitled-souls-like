using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine, IDataPersistence
{
    #region Scripts
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public ForceReciver ForceReciver { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    #endregion

    #region Variables
    [field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float PlayerDetectionRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float StrafeRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public float Knockback { get; private set; }
    [field: SerializeField] public float MaxAttackAttemptTime { get; private set; }
    public GameObject Player { get; private set; }
    public Vector3 startPos { get; private set; }
    #endregion

    [SerializeField] private string id;
    public bool dead;

    [ContextMenu("Generate GUID for Enemies")]
    private void GenerateID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    #region Unity Functions
    private void Awake()
    {
        startPos = transform.position;
    }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        NavMeshAgent.updatePosition = false;
        NavMeshAgent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
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
        SwitchState(new EnemyImpactState(this));
    }
    private void HandleDie()
    {
        gameObject.SetActive(false);
        dead = true;
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AttackRange);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, StrafeRange);
    }

    public void LoadData(GameData data)
    {
        data.enemies.TryGetValue(id, out dead);
        gameObject.SetActive(!dead);
    }

    public void SaveData(ref GameData data)
    {
        if (data.enemies.ContainsKey(id))
        {
            data.enemies.Remove(id);
        }

        data.enemies.Add(id, dead);
    }
}
