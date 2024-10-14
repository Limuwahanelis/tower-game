using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushedState : PlayerState
{
    public static Type StateType { get => typeof(PlayerPushedState); }
    private bool _isInAirAfterPush = false;
    private Collider2D[] _playerCols;
    public PlayerPushedState(GetState function) : base(function)
    {
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        _context.animationManager.PlayAnimation("Idle");
        _context.animationManager.SetAnimator(false);
    }

    public override void Update()
    {
        if (!_context.checks.IsOnGround && !_isInAirAfterPush)
        {
            _isInAirAfterPush = true;
            _context.playerMovement.SetRBMaterial(PlayerMovement.PhysicMaterialType.NO_FRICTION);
        }

        if (_context.checks.IsOnGround && _isInAirAfterPush)
        {
            _context.playerMovement.SetRBMaterial(PlayerMovement.PhysicMaterialType.NONE);
            
            _isInAirAfterPush = false;
            _context.animationManager.SetAnimator(true);
            //if (_playerPusher != null) _playerPusher.ResumeCollisons(_playerCols);
            _context.WaitFrameAndPerformFunction(() => { ChangeState(PlayerIdleState.StateType); });

            return;
        }
    }

    public override void InterruptState()
    {
     
    }

}