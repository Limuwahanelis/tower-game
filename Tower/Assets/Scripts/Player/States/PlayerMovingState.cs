using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerState
{
    public static Type StateType { get => typeof(PlayerMovingState); }
    public PlayerMovingState(GetState function) : base(function)
    {
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        _context.animationManager.PlayAnimation("Move");
    }
    public override void Attack(PlayerCombat.AttackModifiers modifier = PlayerCombat.AttackModifiers.NONE)
    {
        ChangeState(PlayerAttackState.StateType);
    }

    public override void Move(Vector2 direction)
    {
        _context.playerMovement.Move(direction.x);
        if (direction.x == 0) ChangeState(PlayerIdleState.StateType);
    }

    public override void Update()
    {
        PerformInputCommand();
    }

    public override void InterruptState()
    {
     
    }
}