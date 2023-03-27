using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossStateMachine : StateMachine, IDataPersistence
{
    #region Scripts
    [field: Header("Scripts")]
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public ForceReciver ForceReciver { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public WeaponDamage LeftFistWeaponDamage { get; private set; }
    [field: SerializeField] public WeaponDamage RightFistWeaponDamage { get; private set; }
    [field: SerializeField] public WeaponDamage KickWeaponDamage { get; private set; }
    [field: SerializeField] public WeaponDamage GrabWeaponDamage { get; private set; }
    #endregion

    #region Variables
    [field: Header("Variables")]
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [field: SerializeField] public PreThrowHandler PreThrowHandler { get; private set; }
    [field: SerializeField] public float LookSpeed { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float MaxAttackAttemptTime { get; private set; }
    [field: SerializeField] public float MaxWaitToGrabTime { get; private set; }
    [field: SerializeField] public bool Active { get; set; }
    #region Distances
    [field: Header("Distances")]
    [field: SerializeField] public float StrafeDistance { get; private set; }
    [field: SerializeField] public float PunchDistance { get; private set; }
    [field: SerializeField] public float KickDistance { get; private set; }
    [field: SerializeField] public float ThrowDistance { get; private set; }
    #endregion

    public enum Boss { Goblin_King }
    public Boss thisBoss;
    public GameObject Player { get; private set; }
    public Vector3 startPos { get; private set; }
    #endregion

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

        LeftFistWeaponDamage.SetDamage(15, 5);
        RightFistWeaponDamage.SetDamage(15, 5);
        KickWeaponDamage.SetDamage(30, 15);
        GrabWeaponDamage.SetDamage(40, 0);

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
        GetComponent<Drops>().Died();
        gameObject.SetActive(false);
        dead = true;
    }
    #endregion

    #region SaveLoad
    [SerializeField] private string id;
    public bool dead;

    [ContextMenu("Generate GUID for Enemies")]
    private void GenerateID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {
        data.bosses.TryGetValue(id, out dead);
        gameObject.SetActive(!dead);
    }

    public void SaveData(ref GameData data)
    {
        if (data.bosses.ContainsKey(id))
        {
            data.bosses.Remove(id);
        }

        data.bosses.Add(id, dead);
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
