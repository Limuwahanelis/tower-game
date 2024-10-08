using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabbigState : PlayerState
{
    public static Type StateType { get => typeof(PlayerWallGrabbigState); }
    public PlayerWallGrabbigState(GetState function) : base(function)
    {
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        _context.playerMovement.StopPlayer();
        _context.animationManager.Animator.SetBool("Climb wall",true);
        //_context.animationEvents.OnWallClimbed.AddListener(WallClimbed);
        _context.WaitAndPerformFunction( _context.animationManager.GetAnimationLengthRaw("climb 2") + _context.animationManager.GetAnimationLengthRaw("Player wall grab"), () => { WallClimbed(); });
    }

    public override void Update()
    {
        PerformInputCommand();
    }
    private void WallClimbed()
    {
        Logger.Log("CLimbed");
        _context.playerMovement.UnparentSpirtes();
        _context.playerMovement.transform.position = _context.climbTilePos;
        _context.playerMovement.ParentSpirtes();
        _context.animationManager.Animator.SetBool("Climb wall", false);
        _context.playerMovement.SetRB(true);
        ChangeState(PlayerIdleState.StateType);
    }
    public override void InterruptState()
    {
     
    }
}