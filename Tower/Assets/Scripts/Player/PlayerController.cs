using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Debug"), SerializeField] bool _printState;
    public bool IsAlive => _isAlive;
    public PlayerState CurrentPlayerState => _currentPlayerState;
    public GameObject MainBody => _mainBody;
    [Header("Player"), SerializeField] Animator _anim;
    [SerializeField] GameObject _mainBody;
    //[SerializeField] AnimationManager _playerAnimationManager;
    [SerializeField] PlayerMovement _playerMovement;
    //[SerializeField] PlayerChecks _playerChecks;
    //[SerializeField] PlayerCombat _playerCombat;
    //[SerializeField] PlayerCollisions _playerCollisions;
    //[SerializeField] PlayerDodge _playerDodge;
    [SerializeField] PlayerHealthSystem _playerHealthSystem;
    //[SerializeField] ShadowSpikeSkill _shadowSpikeSkill;
    [SerializeField] GameObject _gameOverPanel;
    private PlayerState _currentPlayerState;
    private PlayerContext _context;
    private Dictionary<Type, PlayerState> playerStates = new Dictionary<Type, PlayerState>();
    private bool _isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
