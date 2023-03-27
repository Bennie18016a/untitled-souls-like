using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine, IDataPersistence
{
    #region Scripts
    [field: SerializeField] public ForceReciver ForceReciver { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Stamina Stamina { get; private set; }
    [field: SerializeField] public Stats Stats { get; private set; }
    [field: SerializeField] public UseQuickItem UseQuickItem { get; private set; }
    #endregion

    #region Variables
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public bool CanMove { get; set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float BlockMovementSpeed { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }
    [field: SerializeField] public int DodgeStaminaCost { get; private set; }
    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;
    #endregion
    public Transform MainCameraTransform { get; private set; }
    public Vector3 respawnPoint;

    private void Awake()
    {
        MainCameraTransform = Camera.main.transform;
        respawnPoint = transform.position;
    }

    private void Start()
    {
        //SwitchState(new PlayerRespawnState(this));
        Health.SetMaxHealth(95 + Stats.Health * 5);
        Stamina.SetMaxStamina(40 + Stats.Dexterity * 10); ;

        SwitchState(new PlayerFreeLookState(this));
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

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }
    private void HandleDie()
    {
        SwitchState(new PlayerRespawnState(this));
    }

    public void LoadData(GameData data)
    {
        transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = transform.position;
    }
}
