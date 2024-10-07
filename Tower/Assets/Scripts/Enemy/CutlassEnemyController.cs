using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CutlassEnemyController : EnemyController
{
    [SerializeField] EnemyChecks _checks;
    [SerializeField] EnemyMovement _movement;
    [SerializeField] ObjectDetection _playerFrontDetection;
    [SerializeField] ObjectDetection _playerChaseDetection;
    [SerializeField] ObjectDetection _playerStopChaseDetection;
    protected CutlassEnemyContext _context;
    private EnemyState _pushedState;

    void Start()
    {
        _healthSystem.OnDeath += Kill;
        List<Type> states = AppDomain.CurrentDomain.GetAssemblies().SelectMany(domainAssembly => domainAssembly.GetTypes())
    .Where(type => typeof(EnemyState).IsAssignableFrom(type) && !type.IsAbstract).ToArray().ToList();

        _context = new CutlassEnemyContext
        {
            ChangeEnemyState = ChangeState,
            animMan = _enemyAnimationManager,
            enemyTransform = transform,
            playerTransform = _playerTransform,
            coroutineHolder = this,
            enemyChecks=_checks,
            WaitFrameAndPerformFunction= WaitFrameAndExecuteFunction,
            movement= _movement,
            playerFrontDetection= _playerFrontDetection,
            playerChaseDetection= _playerChaseDetection,
            playerChaseStopDetection= _playerStopChaseDetection,
            spawnPoint=transform.position,
        };
        EnemyState.GetState getState = GetState;
        foreach (Type state in states)
        {
            _enemyStates.Add(state, (EnemyState)Activator.CreateInstance(state, getState));
        }
        _pushedState = GetState(typeof(CutlassEnemyStatePushed));
        _currentEnemyState = GetState(typeof(CutlassEnemyStateIdle));
        _currentEnemyState.SetUpState(_context);
    }

    void Update()
    {
        _currentEnemyState?.Update();
    }
    private void FixedUpdate()
    {
        _currentEnemyState.FixedUpdate();
    }
    public void Push(PushInfo info)
    {
        if (!_healthSystem.IsAlive) return;
        ChangeState(_pushedState);
        ((CutlassEnemyStatePushed)_pushedState).SetUpState(_context,info);
    }
    public void Kill(IDamagable damagable, DamageInfo info)
    {
        ChangeState(GetState(typeof(CutlassEnemyStateDead)));
        _currentEnemyState.SetUpState(_context);
        ((CutlassEnemyStateDead)_currentEnemyState).SetUpState(_context, info);
    }
}
