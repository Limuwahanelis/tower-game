using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Debug"), SerializeField] bool _printState;
    public bool IsAlive => _isAlive;
    public PlayerState CurrentPlayerState => _currentPlayerState;
    public GameObject MainBody => _mainBody;
    [Header("Player")]//, SerializeField] Animator _anim;
    [SerializeField] GameObject _mainBody;
    [SerializeField] AnimationManager _playerAnimationManager;
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] PlayerChecks _playerChecks;
    [SerializeField] PlayerCombat _playerCombat;
    [SerializeField] PlayerAnimationsEvents _playerAnimationsEvents;
    [SerializeField] PlayerSpearWallGrab _playerSpearWallGrab;
    //[SerializeField] PlayerCollisions _playerCollisions;
    //[SerializeField] PlayerDodge _playerDodge;
    [SerializeField] PlayerHealthSystem _playerHealthSystem;
    [SerializeField] PlayerSpearController _playerSpearController;
    //[SerializeField] ShadowSpikeSkill _shadowSpikeSkill;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] PlayerColliders _colliders;
    private PlayerState _currentPlayerState;
    private PlayerContext _context;
    private PlayerState _playerPushedState;
    private Dictionary<Type, PlayerState> playerStates = new Dictionary<Type, PlayerState>();
    private bool _isAlive = true;
    void Start()
    {
        _playerHealthSystem.OnPushed += PushPlayer;
        //_playerHealthSystem.OnDeath += PlayerDeath;
        List<Type> states = AppDomain.CurrentDomain.GetAssemblies().SelectMany(domainAssembly => domainAssembly.GetTypes())
            .Where(type => typeof(PlayerState).IsAssignableFrom(type) && !type.IsAbstract).ToArray().ToList();

        _context = new PlayerContext
        {
            ChangePlayerState = ChangeState,
            animationManager = _playerAnimationManager,
            playerMovement = _playerMovement,
            WaitAndPerformFunction = WaitAndExecuteFunction,
            WaitFrameAndPerformFunction = WaitFrameAndExecuteFunction,
            animationEvents = _playerAnimationsEvents,
            spearWallGrab = _playerSpearWallGrab,
            spearController = _playerSpearController,
            coroutineHolder = this,
            //shadowControlModeSelectionUI = _shadowControlModeSelectionUI,
            checks = _playerChecks,
            combat = _playerCombat,
            colliders=_colliders,
            //canPerformAirCombo = true,
            //collisions = _playerCollisions,
            //playerDodge = _playerDodge,
        };
        PlayerState.GetState getState = GetState;
        foreach (Type state in states)
        {
            playerStates.Add(state, (PlayerState)Activator.CreateInstance(state, getState));
        }
        _playerPushedState = GetState(typeof(PlayerPushedState));
        PlayerState newState = GetState(typeof(PlayerIdleState));
        newState.SetUpState(_context);
        _currentPlayerState = newState;
    }

    public PlayerState GetState(Type state)
    {
        return playerStates[state];
    }
    void Update()
    {
        _currentPlayerState.Update();
    }
    private void FixedUpdate()
    {
        _currentPlayerState.FixedUpdate();
    }
    public void ChangeState(PlayerState newState)
    {
        if (_printState) Logger.Log(newState.GetType());
        _currentPlayerState.InterruptState();
        _currentPlayerState = newState;
    }
    private void PlayerDeath(IDamagable damagable)
    {
        _playerMovement.SetRBMaterial(PlayerMovement.PhysicMaterialType.NONE);
        //PlayerState newState = GetState(typeof(PlayerDeadState));
        //ChangeState(newState);
        //newState.SetUpState(_context); ;
        _gameOverPanel.SetActive(true);
    }
    public void PushPlayer(PushInfo psuhInfo)
    {
        _playerMovement.PushPlayer(psuhInfo);
        ChangeState(_playerPushedState);
        _playerPushedState.SetUpState(_context);
        //_currentPlayerState.Push();
    }
    public Coroutine WaitAndExecuteFunction(float timeToWait, Action function)
    {
        return StartCoroutine(HelperMethods.DelyedFunction(timeToWait, function));
    }
    public Coroutine WaitFrameAndExecuteFunction(Action function)
    {
        return StartCoroutine(HelperMethods.WaitFrame(function));
    }

    private void OnDestroy()
    {
        //_playerHealthSystem.OnDeath -= PlayerDeath;
        //_playerHealthSystem.OnPushed -= PushPlayer;
    }
}
