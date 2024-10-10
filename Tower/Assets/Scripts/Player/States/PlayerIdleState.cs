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
    public override void Attack(PlayerCombat.AttackModifiers modifier = PlayerCombat.AttackModifiers.NONE,bool isHold=false)
    {
        _context.attackModifier = modifier;
        ChangeState(PlayerAttackState.StateType);
    }
    public override void ThrowSpear(PlayerCombat.AttackModifiers modifier = PlayerCombat.AttackModifiers.NONE)
    {
        base.ThrowSpear(modifier);
        switch (modifier)
        {
            case PlayerCombat.AttackModifiers.NONE: _context.spearController.Throw(_context.playerMovement.FlipSide * Vector3.right); break;
            case PlayerCombat.AttackModifiers.UP_ARROW: _context.spearController.Throw( new Vector3(1* _context.playerMovement.FlipSide , 1, 0).normalized); break;
            case PlayerCombat.AttackModifiers.DOWN_ARROW: _context.spearController.Throw(new Vector3(1 * _context.playerMovement.FlipSide, -1,0)); break;
        }

        
    }
    public override void Jump()
    {
        ChangeState(PlayerJumpingState.StateType);
    }
    public override void Update()
    {
        PerformInputCommand();
        if(!_context.checks.IsOnGround) ChangeState(PlayerInAirState.StateType);
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        //_context.animationManager.PlayAnimation("Idle");
        _context.animationManager.Animator.SetBool("Move", false);
        ExecuteSetUpCommand();
        _context.spearController.OnPlayerPulledStarted += StartPlayerPull;
    }
    private void StartPlayerPull()
    {
        ChangeState(PlayerPulledState.StateType);
    }
    public override void InterruptState()
    {
        _context.spearController.OnPlayerPulledStarted -= StartPlayerPull;
    }
}