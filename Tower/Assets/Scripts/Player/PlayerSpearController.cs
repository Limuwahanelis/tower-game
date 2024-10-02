using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpearController : MonoBehaviour
{
    public Action OnSpearReturned;
    public bool CanAttack => !_isThrown;
    [SerializeField] Transform _spearHolder;
    [SerializeField] Transform _player;
    [SerializeField] float _maxThrowRange = 3.8f;
    [SerializeField] float _throwSpeed;
    [SerializeField] PlayerSpearHarpoon _playerHarpoon;
    [SerializeField] SpearChain _spearChain;
    [SerializeField] Animator _animator;
    private bool _isThrown = false;
    private Vector3 _throwDirection;
    private bool _isComingBack = false;
    private bool _hasHitEnemy = false;
    private Vector3 _chainOffset;
    private void Start()
    {
        _chainOffset = _spearChain.transform.position - transform.position;
    }
    private void Update()
    {
        if (_isThrown)
        {
            if (!_isComingBack)
            {
                if (Vector2.Distance(_spearHolder.position, transform.position) < 3.8f)
                {
                    transform.position += _throwDirection * Time.deltaTime * _throwSpeed;
                    //_spearChain.transform.position = transform.position + _chainOffset;
                   // _spearChain.CheckSegmentsForward();
                }
                else 
                {
                    _isComingBack = true;
                }
                if (_hasHitEnemy)
                {
                    _isComingBack = true;
                }
            }
            else
            {
                if (Vector2.Distance(_spearHolder.position, transform.position) > 0.1f)
                {
                    Vector3 dir = (_spearHolder.position - transform.position).normalized;
                    transform.position += dir * Time.deltaTime * _throwSpeed;
                    transform.up = -dir;
                    //_spearChain.transform.position = transform.position + _chainOffset;
                    // _spearChain.CheckSegmentsback();
                }
                else
                {
                    _isComingBack = false;
                    _isThrown = false;
                    transform.position=_spearHolder.position;
                    transform.SetParent(_player);
                    transform.localRotation = Quaternion.Euler(0,0,-90);
                    OnSpearReturned?.Invoke();
                }
            }
        }
    }
    public void Throw(Vector3 direction)
    {
        _animator.enabled = false;
        _playerHarpoon.OnHit += HitEnemy;
        _isThrown = true;
        _throwDirection = direction;
        transform.SetParent(null);
    }
    private void HitEnemy()
    {
        _playerHarpoon.OnHit -= HitEnemy;
        _hasHitEnemy = true;
    }

}
