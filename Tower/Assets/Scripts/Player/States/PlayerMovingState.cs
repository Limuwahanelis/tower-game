using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerCombat;

public class PlayerMovingState : PlayerState
{
    public static Type StateType { get => typeof(PlayerMovingState); }
    public PlayerMovingState(GetState function) : base(function)
    {
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        //_context.animationManager.PlayAnimation("Move");
        _context.animationManager.Animator.SetBool("Move", true);
    }
    public override void Attack(PlayerCombat.AttackModifiers modifier = PlayerCombat.AttackModifiers.NONE)
    {
        _context.attackModifier=modifier;
        ChangeState(PlayerAttackState.StateType);
    }

    public override void Move(Vector2 direction)
    {
        _context.playerMovement.Move(direction.x);
        if (direction.x == 0) ChangeState(PlayerIdleState.StateType);
    }
    public override void Jump()
    {
        base.Jump();
        ChangeState(PlayerJumpingState.StateType);
    }
    public override void Update()
    {
        PerformInputCommand();
    }

    public override void InterruptState()
    {
     
    }
}