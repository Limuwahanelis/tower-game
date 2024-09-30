using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContext
{
    public Action<PlayerState> ChangePlayerState;
    public Func<float, Action, Coroutine> WaitAndPerformFunction;
    public Func<Action, Coroutine> WaitFrameAndPerformFunction;
    public MonoBehaviour coroutineHolder;
    public PlayerMovement playerMovement;
    public AnimationManager animationManager;
    public PlayerSpearWallGrab spearWallGrab;
    public PlayerChecks checks;
    public PlayerCombat combat;
    public PlayerCombat.AttackModifiers attackModifier;
    public PlayerAnimationsEvents animationEvents;
    public Vector3 climbTilePos;
   // public PlayerCollisions collisions;
   // public PlayerDodge playerDodge;
}
