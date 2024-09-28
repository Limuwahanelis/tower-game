using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public static Type StateType { get => typeof(PlayerIdleState); }
    public PlayerIdleState(GetState function) : base(function)
    {
    }
    public override void Move(Vector2 direction)
    {
        if(direction.x!=0) ChangeState(PlayerMovingState.StateType);
    }
    public override void Attack(PlayerCombat.AttackModifiers modifier = PlayerCombat.AttackModifiers.NONE)
    {
        _context.attackModifier = modifier;
        ChangeState(PlayerAttackState.StateType);
    }
    public override void Update()
    {
        PerformInputCommand();
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        //_context.animationManager.PlayAnimation("Idle");
        _context.animationManager.Animator.SetBool("Move", false);
    }

    public override void InterruptState()
    {
     
    }
}