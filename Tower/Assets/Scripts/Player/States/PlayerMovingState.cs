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
        _context.spearController.OnPlayerPulledStarted += StartPlayerPull;
    }
    public override void Attack(PlayerCombat.AttackModifiers modifier = PlayerCombat.AttackModifiers.NONE, bool isHold = false)
    {
        _context.attackModifier=modifier;
        ChangeState(PlayerAttackState.StateType);
    }
    public override void ThrowSpear(AttackModifiers modifier = AttackModifiers.NONE)
    {
        base.ThrowSpear(modifier);
        switch (modifier)
        {
            case PlayerCombat.AttackModifiers.NONE: _context.spearController.Throw(_context.playerMovement.FlipSide * Vector3.right); break;
            case PlayerCombat.AttackModifiers.UP_ARROW: _context.spearController.Throw(new Vector3(1 * _context.playerMovement.FlipSide, 1, 0).normalized); break;
            case PlayerCombat.AttackModifiers.DOWN_ARROW: _context.spearController.Throw(new Vector3(1 * _context.playerMovement.FlipSide, -1, 0)); break;
        }
        
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
    private void StartPlayerPull()
    {
        ChangeState(PlayerPulledState.StateType);
    }
    public override void Update()
    {
        PerformInputCommand();
        if (!_context.checks.IsOnGround) ChangeState(PlayerInAirState.StateType);
    }

    public override void InterruptState()
    {
        _context.spearController.OnPlayerPulledStarted -= StartPlayerPull;
    }
}