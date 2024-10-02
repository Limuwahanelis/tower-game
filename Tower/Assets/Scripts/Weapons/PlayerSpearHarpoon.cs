using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpearHarpoon : MonoBehaviour
{
    public Action OnHit;
    public Action OnHarpoonReturned;
    public bool IsThrown => _isThrown;
    [SerializeField] Transform _player;
    [SerializeField] Transform _spearHolder;
    [SerializeField] float _maxThrowRange = 3.8f;
    [SerializeField] float _throwSpeed;
    [SerializeField] SpearChain _spearChain;
    IPullable _pulledObject;
    private Vector3 _throwDirection;
    private bool _isComingBack = false;
    private bool _hasHitEnemy = false;
    private Vector3 _chainOffset;
    private bool _isThrown = false;
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
                    transform.position = _spearHolder.position;
                    transform.SetParent(_player);
                    transform.localRotation = Quaternion.Euler(0, 0, -90);
                    StopPull();
                    _hasHitEnemy = false;
                    OnHarpoonReturned?.Invoke();
                }
            }
        }
    }
    public void Throw(Vector3 throwDirection)
    {
        _throwDirection = throwDirection;
        _isThrown = true;
        transform.SetParent(null);

    }
    public void StopPull()
    {
        if (_pulledObject == null) return;
        _pulledObject.EndPull();
        _pulledObject = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPullable pull = collision.GetComponent<IPullable>();
        if (pull!=null)
        {
            _pulledObject = pull;
            _pulledObject.StartPull(transform);
            _hasHitEnemy = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_pulledObject != null) return;
        IPullable pull = collision.GetComponent<IPullable>();
        if (pull != null)
        {
            _pulledObject = pull;
            _pulledObject.StartPull(transform);
            _hasHitEnemy = true;
        }
    }
}
