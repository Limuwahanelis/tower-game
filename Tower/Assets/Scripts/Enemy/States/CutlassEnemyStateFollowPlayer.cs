using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutlassEnemyStateFollowPlayer : EnemyState
{
    public static Type StateType { get => typeof(CutlassEnemyStateFollowPlayer); }
    private CutlassEnemyContext _context;
    public CutlassEnemyStateFollowPlayer(GetState function) : base(function)
    {
    }

    public override void SetUpState(EnemyContext context)
    {
        base.SetUpState(context);
        _context = (CutlassEnemyContext)context;
        _context.chasePlayer = true;
        _context.playerChaseDetection.OnObjectLeftdUnity.AddListener(PlayerLeft);
        _context.playerFrontDetection.OnObjectDetectedUnity.AddListener(PlayerInAttackRange);
    }

    public override void Update()
    {
        if (_context.enemyTransform.position.x < _context.playerTransform.position.x) _context.movement.Move(Vector2.right);
        else _context.movement.Move(Vector2.left);
    }
    private void PlayerInAttackRange()
    {
        _context.attackPlayer = true;
        _context.chasePlayer = false;
        ChangeState(CutlassEnemyStateIdle.StateType);
    }
    private void PlayerLeft()
    {
        _context.attackPlayer = false;
        _context.chasePlayer = false;
        ChangeState(CutlassEnemyStateGoBackToSpawn.StateType);
    }
    public override void InterruptState()
    {
        _context.playerChaseDetection.OnObjectLeftdUnity.RemoveListener(PlayerLeft);
        _context.playerFrontDetection.OnObjectDetectedUnity.RemoveListener(PlayerInAttackRange);
    }
}