using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutlassEnemyStateDead : EnemyState
{
    public static Type StateType { get => typeof(CutlassEnemyStateDead); }
    private CutlassEnemyContext _context;
    public CutlassEnemyStateDead(GetState function) : base(function)
    {
    }

    public override void SetUpState(EnemyContext context)
    {
        base.SetUpState(context);
        _context = (CutlassEnemyContext)context;
    }
    public void SetUpState(EnemyContext context,DamageInfo info)
    {
        base.SetUpState(context);
        _context = (CutlassEnemyContext)context;
        // sub result - <0 mewans palyer is on right, else its on left. mult result - <0 player is in front, else player is behind
        if ((_enemyContext.enemyTransform.position.x - info.dmgPosition.x) * ((int)_enemyContext.movement.FlipSide) < 0)
        {
            if(_enemyContext.movement.FlipSide==GlobalEnums.HorizontalDirections.RIGHT) _context.animMan.Animator.SetTrigger("Dead Left");
            else if (_enemyContext.movement.FlipSide == GlobalEnums.HorizontalDirections.LEFT) _context.animMan.Animator.SetTrigger("Dead Right");
        }
        else
        {
            if (_enemyContext.movement.FlipSide == GlobalEnums.HorizontalDirections.RIGHT) _context.animMan.Animator.SetTrigger("Dead Right");
            else if (_enemyContext.movement.FlipSide == GlobalEnums.HorizontalDirections.LEFT) _context.animMan.Animator.SetTrigger("Dead Left");
        }
        _context.movement.SetRB(false);
    }
    public override void Update()
    {

    }

    public override void InterruptState()
    {
     
    }
}