using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonAttackInputCommand : InputCommand
{
    private PlayerCombat.AttackModifiers _attackModifier;
    public HarpoonAttackInputCommand(PlayerState playerState, PlayerCombat.AttackModifiers attackModifier = PlayerCombat.AttackModifiers.NONE) : base(playerState)
    {
        _attackModifier = attackModifier;
    }
    public override void Execute(PlayerState playerState)
    {
        playerState.ThrowSpear(_attackModifier);
    }
    public override void Execute()
    {
        _playerState.ThrowSpear(_attackModifier);
    }

    public override void Undo()
    {
        _playerState.UndoComand(typeof( HarpoonAttackInputCommand));
    }
}
