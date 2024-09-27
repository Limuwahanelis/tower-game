using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public static Type StateType { get => typeof(PlayerAttackState); }

    protected float _animSpeed;
    protected float _animLength;
    public PlayerAttackState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        PerformInputCommand();
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        _context.animationManager.PlayAnimation("Attack 1");
        _animSpeed = _context.animationManager.GetAnimationSpeed("Attack 1");
        _animLength = _context.animationManager.GetAnimationLength("Attack 1")/_animSpeed;

        _context.WaitAndPerformFunction(_animLength, () =>
        {
            ChangeState(PlayerIdleState.StateType);
        }
        );
    }

    public override void InterruptState()
    {
     
    }
}