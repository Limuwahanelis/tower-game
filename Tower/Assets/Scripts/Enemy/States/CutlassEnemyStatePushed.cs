using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutlassEnemyStatePushed : EnemyState
{
    public static Type StateType { get => typeof(CutlassEnemyStatePushed); }
    private CutlassEnemyContext _context;
    private bool _isOffGround = false;
    private bool _chaseAfterPlayer=false;
    public CutlassEnemyStatePushed(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        if (!_enemyContext.enemyChecks.IsTouchingGround && !_isOffGround)
        {
            _isOffGround = true;
        }
        if (_enemyContext.enemyChecks.IsTouchingGround && _isOffGround)
        {
            _isOffGround = false;
            _enemyContext.WaitFrameAndPerformFunction(() => { ChangeState(CutlassEnemyStateIdle.StateType); });
        }
    }
    public void SetUpState(EnemyContext context, PushInfo damageInfo)
    {
        base.SetUpState(context);
        _context = (CutlassEnemyContext)context;
        _enemyContext.animMan.SetAnimator(false);
        _enemyContext.movement.Stop();
        // sub result - <0 mewans palyer is on right, else its on left. mult result - <0 player is in front, else player is behind
        if ((_enemyContext.enemyTransform.position.x - damageInfo.pushPosition.x) * ((int)_enemyContext.movement.FlipSide) > 0) _enemyContext.movement.Push(-1);
        else _enemyContext.movement.Push();
        _isOffGround = false;
        _context.playerFrontDetection.OnObjectLeftdUnity.AddListener(PlayerLeft);
        _context.playerFrontDetection.OnObjectDetectedUnity.AddListener(PlayerEnteredAttackRange);
        _context.playerChaseDetection.OnObjectDetectedUnity.AddListener(ChasePlayer);
        _context.playerChaseStopDetection.OnObjectLeftdUnity.AddListener(StopChase);

    }
    private void PlayerLeft()
    {
        _context.attackPlayer = false;
        _context.chasePlayer = true;
    }
    private void PlayerEnteredAttackRange()
    {
        _context.attackPlayer = true;
    }
    private void ChasePlayer()
    {
        _context.chasePlayer = true;
    }
    private void StopChase()
    {
        _context.chasePlayer = false;
    }
    public override void SetUpState(EnemyContext context)
    {
        base.SetUpState(context);
        _enemyContext.animMan.SetAnimator(false);
        _enemyContext.movement.Stop();
        _enemyContext.movement.Push();
        _isOffGround = false;
    }

    public override void InterruptState()
    {
        _enemyContext.animMan.SetAnimator(true);
        _context.playerFrontDetection.OnObjectLeftdUnity.RemoveListener(PlayerLeft);
        _context.playerFrontDetection.OnObjectDetectedUnity.RemoveListener(PlayerEnteredAttackRange);
        _context.playerChaseDetection.OnObjectDetectedUnity.RemoveListener(ChasePlayer);
        _context.playerChaseStopDetection.OnObjectLeftdUnity.RemoveListener(StopChase);
    }
}