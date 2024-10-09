using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public static Type StateType { get => typeof(PlayerInAirState); }
    public PlayerInAirState(GetState function) : base(function)
    {
    }
    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        _context.playerMovement.SetRBMaterial(PlayerMovement.PhysicMaterialType.NO_FRICTION);
        _context.spearController.OnPlayerPulledStarted += StartPlayerPull;
    }
    public override void Update()
    {
        PerformInputCommand();
        if (_context.checks.IsOnGround && math.abs(_context.playerMovement.PlayerRB.velocity.y) < 0.0004)
        {
            _context.playerMovement.SetRBMaterial(PlayerMovement.PhysicMaterialType.NORMAL);
            if (_stateTypeToChangeFromInputCommand != null)
            {
                ChangeState(_stateTypeToChangeFromInputCommand);
                _stateTypeToChangeFromInputCommand = null;
            }
            else ChangeState(PlayerIdleState.StateType);
        }

    }
    public override void Attack(PlayerCombat.AttackModifiers modifier = PlayerCombat.AttackModifiers.NONE)
    {
        //if(modifier==PlayerCombat.AttackModifiers.UP_ARROW)
        //{
        //    _context.animationManager.Animator.SetTrigger("Up attack");
        //    _context.spearWallGrab.SetWallGrab(true);
        //    _context.WaitAndPerformFunction(_context.animationManager.GetAnimationLength("Attack UR 1"), () => 
        //    {
        //        _context.spearWallGrab.SetWallGrab(false);
        //        _context.playerMovement.OnWallGrab -= Grabwall; 
        //    });
        //    _context.playerMovement.OnWallGrab += Grabwall;
        //}
        // else _stateTypeToChangeFromInputCommand = PlayerAttackState.StateType;
        _context.attackModifier = modifier;
      ChangeState(PlayerAttackState.StateType);

        //string attackTrigger = "Normal attack";
        //string animName = "Attack 1";
        //_context.spearWallGrab.SetWallGrab(true);
        //float _animLength = _context.animationManager.GetAnimationLength(animName);
        //switch (modifier)
        //{
        //    case PlayerCombat.AttackModifiers.UP_ARROW:
        //        {
        //            attackTrigger = "Up attack";
        //            animName = "Attack UR 1";
        //            _animLength = _context.animationManager.GetAnimationLengthRaw("Attack UR 1");
        //            //_context.playerMovement.OnWallGrab += Grabwall;
        //            break;
        //        }
        //    case PlayerCombat.AttackModifiers.DOWN_ARROW:
        //        {
        //            attackTrigger = "Down attack";
        //            animName = "Attack DR 1";
        //            _animLength = _context.animationManager.GetAnimationLengthRaw("Attack DR 1");
        //            break;
        //        }
        //}
        //_context.WaitAndPerformFunction(_context.animationManager.GetAnimationLength(animName), () =>
        //{
        //    _context.spearWallGrab.SetWallGrab(false);
        //    _context.playerMovement.OnWallGrab -= Grabwall;
        //});
        //_context.playerMovement.OnWallGrab += Grabwall;
        //_context.animationManager.Animator.SetTrigger(attackTrigger);
    }
    public override void ThrowSpear(PlayerCombat.AttackModifiers modifier = PlayerCombat.AttackModifiers.NONE)
    {
        base.ThrowSpear(modifier);
        switch (modifier)
        {
            case PlayerCombat.AttackModifiers.NONE: _context.spearController.Throw(_context.playerMovement.FlipSide * Vector3.right); break;
            case PlayerCombat.AttackModifiers.UP_ARROW: _context.spearController.Throw(new Vector3(1 * _context.playerMovement.FlipSide, 1, 0).normalized); break;
            case PlayerCombat.AttackModifiers.DOWN_ARROW: _context.spearController.Throw(new Vector3(1 * _context.playerMovement.FlipSide, -1, 0)); break;
        }
    }
    private void Grabwall(Vector3 tilePos)
    {
        _context.playerMovement.OnWallGrab -= Grabwall;
        _context.spearWallGrab.SetWallGrab(false);
        _context.climbTilePos = tilePos;
        _stateTypeToChangeFromInputCommand = null;
        ChangeState(PlayerWallGrabbigState.StateType);
    }
    public override void Jump()
    {
        _stateTypeToChangeFromInputCommand = PlayerJumpingState.StateType;
    }
    public override void Move(Vector2 direction)
    {
        _context.playerMovement.Move(direction.x, true);
    }
    private void StartPlayerPull()
    {
        ChangeState(PlayerPulledState.StateType);
    }
    public override void UndoComand(Type inputCommand)
    {
        if (inputCommand == typeof(HarpoonAttackInputCommand)) SetCommandOnNextSetUp(null);
        _stateTypeToChangeFromInputCommand = null;
    }
    public override void InterruptState()
    {
        _context.spearWallGrab.SetWallGrab(false);
        _context.playerMovement.OnWallGrab -= Grabwall;
    }
}