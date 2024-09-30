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
        _animSpeed = _context.animationManager.GetAnimationSpeed("Attack 1");
        _animLength = _context.animationManager.GetAnimationLength("Attack 1") / _animSpeed;
        switch (modifier)
        {
            case PlayerCombat.AttackModifiers.UP_ARROW:
                {
                    attackTrigger = "Up attack";
                    _animSpeed = _context.animationManager.GetAnimationSpeed("Attack UR 1");
                    _animLength = _context.animationManager.GetAnimationLength("Attack UR 1") / _animSpeed;
                    _context.playerMovement.OnWallGrab += Grabwall;
                    break;
                }
            case PlayerCombat.AttackModifiers.DOWN_ARROW:
                {
                    attackTrigger = "Down attack";
                    _animSpeed = _context.animationManager.GetAnimationSpeed("Attack DR 1");
                    _animLength = _context.animationManager.GetAnimationLength("Attack DR 1") / _animSpeed;
                    break;
                }
        }
        _context.animationManager.Animator.SetTrigger(attackTrigger);
        _context.combat.OnAttackEnded += AttackEnd;
    }
    private void Grabwall(Vector3 tilePos)
    {
        _context.playerMovement.OnWallGrab -= Grabwall;
        _context.climbTilePos = tilePos;
        ChangeState(PlayerWallGrabbigState.StateType);
    }
    private void AttackEnd()
    {
        _context.combat.OnAttackEnded -= AttackEnd;
        ChangeState(PlayerIdleState.StateType);
    }
    public override void InterruptState()
    {
        _context.combat.OnAttackEnded -= AttackEnd;
        _context.playerMovement.OnWallGrab -= Grabwall;
    }
}