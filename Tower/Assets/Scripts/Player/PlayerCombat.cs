using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public enum AttackModifiers
    {

        NONE, UP_ARROW, DOWN_ARROW,
    }
    public Action OnAttackEnded;
    [SerializeField] Weapon _playerWeapon;
    [SerializeField] GameObject _weaponObject;
    [SerializeField] Transform _rightHandWeaponHold;
    [SerializeField] Transform _backWeaponHold;

    //[SerializeField] ComboList _comboList1;
    //[SerializeField] Player _player;
    [SerializeField] PlayerAnimationsEvents _playerAnimatorEvents;
    private int _comboAttackCounter = 1;

    private void Awake()
    {
        _playerAnimatorEvents.OnStartCheckForEnemyColliders.AddListener(StartCheckingForEnemyCollider);
        _playerAnimatorEvents.OnStopCheckForEnemyColliders.AddListener(StopCheckingForEnemyCollider);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void StartCheckingForEnemyCollider()
    {
        SetCheckForEnemies(true);
    }
    private void StopCheckingForEnemyCollider()
    {
        OnAttackEnded?.Invoke();
        SetCheckForEnemies(false);
    }
    public void SetCheckForEnemies(bool value)
    {
        _playerWeapon.SetCheckForCollisions(value);
    }

    public void AttachWeaponToRightHand()
    {
        _weaponObject.transform.position = _rightHandWeaponHold.transform.position;
        _weaponObject.transform.parent = _rightHandWeaponHold;
        _weaponObject.transform.localRotation = Quaternion.identity; //_backWeaponHold.transform.rotation;
    }
    public void MoveWeaponToSeath()
    {
        _weaponObject.transform.position = _backWeaponHold.transform.position;
        _weaponObject.transform.parent = _backWeaponHold;
        _weaponObject.transform.localRotation = Quaternion.identity; //_backWeaponHold.transform.rotation;
    }
    public void ResetComboCounter()
    {
        _comboAttackCounter = 1;
    }
    public void PerformNextAttackInCombo()//in PlayerAttackingState attackState)
    {
        //if (_comboAttackCounter > _comboList1.comboList.Count)
        //{
        //    return;
        //}
        //if (_player.animManager.GetAnimationCurrentDuration("Attack " + _comboAttackCounter, "Combat") >= _comboList1.comboList[_comboAttackCounter - 1].nextAttackWindowStart / _player.animManager.GetAnimationSpeed("Attack " + _comboAttackCounter, "Combat") && _player.animManager.GetAnimationCurrentDuration("Attack " + _comboAttackCounter, "Combat") <= _comboList1.comboList[_comboAttackCounter - 1].nextAttackWindowEnd / _player.animManager.GetAnimationSpeed("Attack " + _comboAttackCounter, "Combat"))
        //{
        //    //_player.animManager.PlayAnimation("Attack " + _comboAttackCounter);
        //    _player.anim.SetTrigger("Attack");
        //    _comboAttackCounter++;
        //    attackState.StartWaitingFoNextAttack("Attack " + _comboAttackCounter);
        //}
    }
    //public float GetCurrentAttackWindowEndRaw()
    //{
    //    return _comboList1.comboList[_comboAttackCounter - 1].nextAttackWindowEnd;
    //}
    public void ResetWeaponDamageables()
    {
        _playerWeapon.ResetTargets();
    }
    private void OnDestroy()
    {
        _playerAnimatorEvents.OnStartCheckForEnemyColliders.RemoveListener(StartCheckingForEnemyCollider);
        _playerAnimatorEvents.OnStopCheckForEnemyColliders.RemoveListener(StopCheckingForEnemyCollider);
    }
}
