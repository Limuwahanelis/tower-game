using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInputCommand : InputCommand
{
    private PlayerCombat.AttackModifiers _attackModifier;
    private bool _isHold;
    public AttackInputCommand(PlayerState playerState, PlayerCombat.AttackModifiers attackModifier = PlayerCombat.AttackModifiers.NONE,bool isHold=false) : base(playerState)
    {
        _attackModifier = attackModifier;
        _isHold = isHold;
    }

    public override void Execute()
    {
        _playerState.Attack(_attackModifier,_isHold);
    }
    public override void Execute(PlayerState state)
    {
        state.Attack(_attackModifier, _isHold);
    }
    public override void Undo()
    {
        _playerState.UndoComand(typeof(AttackInputCommand));
    }
}
