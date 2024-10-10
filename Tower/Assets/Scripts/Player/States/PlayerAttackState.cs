using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public static Type StateType { get => typeof(PlayerAttackState); }

    protected float _animSpeed;
    protected float _animLength;
    private bool _nextAttack;
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
        if(!_context.spearController.CanAttack)
        {
            ChangeState(PlayerIdleState.StateType);
            return;
        }
        SetAttack(context.attackModifier);
    }
    public override void Attack(PlayerCombat.AttackModifiers modifier = PlayerCombat.AttackModifiers.NONE)
    {
        SetAttack(modifier);
    }
    public override void Move(Vector2 direction)
    {
        
        _context.playerMovement.Move(direction.x);
    }
    private void SetAttack(PlayerCombat.AttackModifiers modifier)
    {
        string attackTrigger = "Normal attack";
        string animName = "Attack 1";
        _context.spearWallGrab.SetWallGrab(true);
        switch (modifier)
        {
            case PlayerCombat.AttackModifiers.UP_ARROW:
                {
                    attackTrigger = "Up attack";
                    animName = "Attack UR 1";
                    
                    break;
                }
            case PlayerCombat.AttackModifiers.DOWN_ARROW:
                {
                    attackTrigger = "Down attack";
                    animName = "Attack DR 1";
                    break;
                }
        }
        _context.WaitAndPerformFunction(_context.animationManager.GetAnimationLength(animName), () =>
        {
            _context.spearWallGrab.SetWallGrab(false);
            _context.playerMovement.OnWallGrab -= Grabwall;
        });
        _context.playerMovement.OnWallGrab += Grabwall;
        _context.animationManager.Animator.SetTrigger(attackTrigger);
        _context.combat.OnAttackEnded += AttackEnd;
    }
    private void Grabwall(Vector3 tilePos)
    {
        Logger.Log("Grabbeds");
        _context.playerMovement.OnWallGrab -= Grabwall;
        _context.climbTilePos = tilePos;
        ChangeState(PlayerWallGrabbigState.StateType);
    }
    private void AttackEnd()
    {
        Logger.Log("End atack");
        _context.combat.OnAttackEnded -= AttackEnd;
        //_context.playerMovement.OnWallGrab -= Grabwall;
        ChangeState(PlayerIdleState.StateType);
    }
    public override void InterruptState()
    {
        _context.combat.OnAttackEnded -= AttackEnd;
        _context.playerMovement.OnWallGrab -= Grabwall;
    }
}