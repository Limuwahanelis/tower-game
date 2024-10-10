using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    [SerializeField] PlayerController _player;
    [SerializeField] InputActionAsset _controls;
    [SerializeField] bool _useCommands;
    [SerializeField] PlayerInputStack _inputStack;
    private Vector2 _direction;
    //Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.IsAlive)
        {

           //if (!PauseSettings.IsGamePaused)
           //
                _player.CurrentPlayerState.Move(_direction);

           //
        }
    }
    private void OnMove(InputValue value)
    {
        _direction = value.Get<Vector2>();

    }
    void OnJump(InputValue value)
    {
        //if (PauseSettings.IsGamePaused) return;
        if (_useCommands) _inputStack.CurrentCommand = new JumpInputCommand(_player.CurrentPlayerState);
        else _player.CurrentPlayerState.Jump();

    }
    private void OnAttack(InputValue value)
    {
        Logger.Log(value.Get<float>());
       // if (PauseSettings.IsGamePaused) return;
        if (_useCommands)
        {
            _inputStack.CurrentCommand = new AttackInputCommand(_player.CurrentPlayerState);
            if (_direction.y > 0) _inputStack.CurrentCommand = new AttackInputCommand(_player.CurrentPlayerState, PlayerCombat.AttackModifiers.UP_ARROW);
            if (_direction.y < 0) _inputStack.CurrentCommand = new AttackInputCommand(_player.CurrentPlayerState, PlayerCombat.AttackModifiers.DOWN_ARROW);
        }
        else
        {

            if (_direction.y == 0) _player.CurrentPlayerState.Attack();
            else if (_direction.y > 0) _player.CurrentPlayerState.Attack(PlayerCombat.AttackModifiers.UP_ARROW);
            else if (_direction.y < 0) _player.CurrentPlayerState.Attack(PlayerCombat.AttackModifiers.DOWN_ARROW);
        }
    }
    private void OnHarpoon(InputValue value)
    {
        if (!PlayerAbilities.UnlockedAbilities[((int)PlayerAbilities.Abilites.HARPOON)]) return;
        if (_useCommands)
        {
            _inputStack.CurrentCommand = new HarpoonAttackInputCommand(_player.CurrentPlayerState);
            if (_direction.y > 0) _inputStack.CurrentCommand = new HarpoonAttackInputCommand(_player.CurrentPlayerState, PlayerCombat.AttackModifiers.UP_ARROW);
            if (_direction.y < 0) _inputStack.CurrentCommand = new HarpoonAttackInputCommand(_player.CurrentPlayerState, PlayerCombat.AttackModifiers.DOWN_ARROW);
        }
        else
        {
            if (_direction.y == 0) _player.CurrentPlayerState.ThrowSpear();
            else if (_direction.y > 0) _player.CurrentPlayerState.ThrowSpear(PlayerCombat.AttackModifiers.UP_ARROW);
            else if (_direction.y < 0) _player.CurrentPlayerState.ThrowSpear(PlayerCombat.AttackModifiers.DOWN_ARROW);
        }
        //if (PlayerAbilities.UnlockedAbilities[((int)PlayerAbilities.Abilites.HARPOON)]) 
        //{

        //}
    }
}
