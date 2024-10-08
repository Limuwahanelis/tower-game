using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpearController : MonoBehaviour
{
    public Action OnSpearReturned;
    public bool CanAttack => !_playerHarpoon.IsThrown;
   
    [SerializeField] Transform _player;
    [SerializeField] float _maxThrowRange = 3.8f;
    [SerializeField] float _throwSpeed;
    [SerializeField] PlayerSpearHarpoon _playerHarpoon;
    [SerializeField] Animator _animator;

    private void Start()
    {
       
    }
    private void Update()
    {

    }
    public void Throw(Vector3 direction)
    {
        gameObject.SetActive(false);
        _playerHarpoon.gameObject.SetActive(true);
        _playerHarpoon.OnHarpoonReturned += OnHarpoonReturn;
        _playerHarpoon.Throw(direction);
        _animator.enabled = false;


    }

    private void OnHarpoonReturn()
    {
        _playerHarpoon.OnHarpoonReturned -= OnHarpoonReturn;
        _animator.enabled = true;
        gameObject.SetActive(true);
        _playerHarpoon.gameObject.SetActive(false);
    }
}
