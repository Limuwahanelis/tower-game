using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CutlassEnemyStateGoBackToSpawn : EnemyState
{
    public static Type StateType { get => typeof(CutlassEnemyStateGoBackToSpawn); }
    private CutlassEnemyContext _context;
    public CutlassEnemyStateGoBackToSpawn(GetState function) : base(function)
    {
    }

    public override void SetUpState(EnemyContext context)
    {
        base.SetUpState(context);
        _context = (CutlassEnemyContext)context;
        _context.playerFrontDetection.OnObjectDetectedUnity.AddListener(AttackPlauer);
        _context.playerChaseDetection.OnObjectDetectedUnity.AddListener(ChasePlayer);
    }
    public override void FixedUpdate()
    {
        if (math.abs(_context.enemyTransform.position.x - _context.spawnPoint.x) > 0.01f)
        {
            if (_context.enemyTransform.position.x < _context.spawnPoint.x) _context.movement.Move(Vector2.right);
            else _context.movement.Move(Vector2.left);
        }
        else ChangeState(CutlassEnemyStateIdle.StateType);
    }
    private void AttackPlauer()
    {
        _context.attackPlayer = true;
        ChangeState(CutlassEnemyStateIdle.StateType);
    }
    private void ChasePlayer()
    {
        ChangeState(CutlassEnemyStateFollowPlayer.StateType);
    }
    public override void InterruptState()
    {
        _context.playerFrontDetection.OnObjectDetectedUnity.RemoveListener(AttackPlauer);
        _context.playerChaseDetection.OnObjectDetectedUnity.RemoveListener(ChasePlayer);
    }

    public override void Update()
    {
        
    }
}