using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutlassEnemyStateIdle : EnemyState
{
    public static Type StateType { get => typeof(CutlassEnemyStateIdle); }
    private CutlassEnemyContext _context;
    private float _attackCD;
    private float _time = 0;
    public CutlassEnemyStateIdle(GetState function) : base(function)
    {
    }
    public override void SetUpState(EnemyContext context)
    {
        base.SetUpState(context);
        _context = (CutlassEnemyContext)context;
        _context.playerFrontDetection.OnObjectLeftdUnity.AddListener(PlayerLeft);
        _context.playerFrontDetection.OnObjectDetectedUnity.AddListener(PlayerEnteredAttackRange);
        _context.playerChaseDetection.OnObjectDetectedUnity.AddListener(ChasePlayer);
        _attackCD = 1f+_context.animMan.GetAnimationLength("Attack");
        if (Vector2.Distance(_context.spawnPoint, _context.enemyTransform.position) > 0.1f && _context.attackPlayer == false)
        {
            ChangeState(CutlassEnemyStateGoBackToSpawn.StateType);
            return;
        }
        if (_context.chasePlayer)
        {
            _context.chasePlayer = false;
            ChangeState(CutlassEnemyStateFollowPlayer.StateType);
            return;
        }

    }
    public override void Update()
    {
        if (!_context.attackPlayer) return;
        if(_time>_attackCD)
        {
            _context.animMan.Animator.SetTrigger("Attack");
            _time = 0;
        }
        _time += Time.deltaTime;
    }
    private void PlayerLeft()
    {
        ChangeState(CutlassEnemyStateFollowPlayer.StateType);
    }
    private void PlayerEnteredAttackRange()
    {
        _context.attackPlayer = true;
    }
    private void ChasePlayer()
    {
        //if(_context.attackPlayer)
        ChangeState(CutlassEnemyStateFollowPlayer.StateType);
    }
    public override void InterruptState()
    {
        _context.playerFrontDetection.OnObjectLeftdUnity.RemoveListener(PlayerLeft);
        _context.playerFrontDetection.OnObjectDetectedUnity.RemoveListener(PlayerEnteredAttackRange);
        _context.playerChaseDetection.OnObjectDetectedUnity.RemoveListener(ChasePlayer);
        _context.attackPlayer = false;
    }
}