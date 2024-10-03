using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerJumpingState : PlayerState
{
    public static Type StateType { get => typeof(PlayerJumpingState); }
    private float _timerCheck = 0.2f;
    private float _timer = 0;
    public PlayerJumpingState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        PerformInputCommand();
        _timer += Time.deltaTime;
        if(_timer >= _timerCheck ) 
        {
            ChangeState(PlayerIdleState.StateType);
            return;
        }
        if(!_context.checks.IsOnGround)
        {
            _context.playerMovement.SetRBMaterial(PlayerMovement.PhysicMaterialType.NO_FRICTION);
            ChangeState(PlayerInAirState.StateType);
        }
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        _context.playerMovement.Jump();
        _timer = 0;
    }

    public override void InterruptState()
    {
    }

    public override void UndoComand(Type inputcommand)
    {

    }
}