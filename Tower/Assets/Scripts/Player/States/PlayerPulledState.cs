using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPulledState : PlayerState
{
    public static Type StateType { get => typeof(PlayerPulledState); }
    public PlayerPulledState(GetState function) : base(function)
    {
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        _context.spearController.OnPlayerPulledEnded += EndPull;
        _context.colliders.SetCollisionsColliders(false);
        _context.playerMovement.StopPlayer();
        _context.playerMovement.SetRB(false);
    }
    public override void Update()
    {
        PerformInputCommand();
    }
    public override void Attack(PlayerCombat.AttackModifiers modifier = PlayerCombat.AttackModifiers.NONE, bool isHold = false)
    {
        base.Attack(modifier);
        SetCommandOnNextSetUp(new AttackInputCommand(this, modifier));
    }
    private void EndPull()
    {
        ChangeState(PlayerIdleState.StateType);
    }
    public override void InterruptState()
    {
        _context.spearController.OnPlayerPulledEnded -= EndPull;
        _context.colliders.SetCollisionsColliders(true);
        _context.playerMovement.SetRB(true);
    }
}