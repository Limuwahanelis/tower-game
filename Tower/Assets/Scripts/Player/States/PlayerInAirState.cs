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
        if(modifier==PlayerCombat.AttackModifiers.UP_ARROW)
        {
            _context.animationManager.Animator.SetTrigger("Up attack");
            _context.spearWallGrab.SetWallGrab(true);
            _context.WaitAndPerformFunction(_context.animationManager.GetAnimationLength("Attack UR 1"), () => 
            {
                _context.spearWallGrab.SetWallGrab(false);
                _context.playerMovement.OnWallGrab -= Grabwall; 
            });
            _context.playerMovement.OnWallGrab += Grabwall;
        }
       else _stateTypeToChangeFromInputCommand = PlayerAttackState.StateType;
    }
    public override void ThrowSpear(PlayerCombat.AttackModifiers modifier = PlayerCombat.AttackModifiers.NONE)
    {
        SetCommandOnNextSetUp(new HarpoonAttackInputCommand(this, modifier));
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