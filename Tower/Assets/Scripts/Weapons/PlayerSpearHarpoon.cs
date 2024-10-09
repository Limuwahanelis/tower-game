using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpearHarpoon : MonoBehaviour
{
    public Action OnHit;
    public Action OnPlayerPullStarted;
    public Action OnPlayerPullEnded;
    public Action OnHarpoonReturned;
    public bool IsThrown => _isThrown;
    [SerializeField] Transform _playerMainBody;
    [SerializeField] Transform _playerController;
    [SerializeField] Transform _spearHolder;
    [SerializeField] float _maxThrowRange = 3.8f;
    [SerializeField] float _throwSpeed;
    [SerializeField] SpearChain _spearChain;
    [SerializeField] Transform _harpoonTip;
    IPullable _pulledObject;
    private Vector3 _throwDirection;
    private bool _isComingBack = false;
    private bool _hasHitSomething = false;
    private Vector3 _chainOffset;
    private bool _isThrown = false;
    private bool _pullPlayer;
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
                    Vector3 dir = (_spearHolder.position - transform.position);
                    Vector3 dirNor = dir.normalized;
                   //float angle= Vector2.SignedAngle(dirNor, new Vector2(_spearHolder.position.x,_spearHolder.position.y- transform.position.y));
                   // Logger.Log(angle);
                   // transform.RotateAround(_harpoonTip.position, _spearHolder.forward, -angle);
                   
                    transform.position += _throwDirection * Time.deltaTime * _throwSpeed;
                    if(dir!=Vector3.zero) transform.up = -dirNor;
                    _spearChain.transform.position = transform.position + _chainOffset;
                     _spearChain.CheckSegmentsForward();
                }
                else
                {
                    _isComingBack = true;
                }
                if (_hasHitSomething)
                {
                    _isComingBack = true;
                }
            }
            else
            {
                if (Vector2.Distance(_spearHolder.position, transform.position) > 0.1f)
                {
                    Vector3 dir = (_spearHolder.position - transform.position);
                    Vector3 dirNor = dir.normalized;
                    if (_pullPlayer)
                    {
                        _playerController.transform.position += -dirNor * Time.deltaTime * _throwSpeed;
                        _spearChain.transform.position = transform.position + _chainOffset;
                        _spearChain.CheckSegmentsback();
                    }
                    else
                    {
                        transform.position += dirNor * Time.deltaTime * _throwSpeed;
                        //float angle = Vector2.SignedAngle(dirNor, new Vector2(_spearHolder.position.x, _spearHolder.position.y - transform.position.y));
                        //Logger.Log(angle);
                        //transform.RotateAround(_harpoonTip.position, _spearHolder.forward, -angle);
                        transform.up = -dir;
                        _spearChain.transform.position = transform.position + _chainOffset;
                        _spearChain.CheckSegmentsback();
                    }
                }
                else
                {
                    _isComingBack = false;
                    _isThrown = false;
                    if (_pullPlayer) OnPlayerPullEnded?.Invoke();
                    _pullPlayer = false;
                    _spearChain.transform.localPosition = Vector3.zero;
                    _spearChain.ResetChain();
                    transform.position = _spearHolder.position;
                    transform.SetParent(_playerMainBody);
                    transform.localRotation = Quaternion.Euler(0, 0, -90);
                    StopPull();
                    _hasHitSomething = false;
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
        if(_isComingBack) return;
        _hasHitSomething = true;
        IPullable pull = collision.GetComponent<IPullable>();
        if (pull!=null)
        {
            _pulledObject = pull;
            _pulledObject.StartPull(transform);
          
        }
        else if(collision.GetComponent<IPlayerPullable>() != null) 
        {
            OnPlayerPullStarted?.Invoke();
            _pullPlayer = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isComingBack) return;
        _hasHitSomething = true;
        if (_pulledObject != null) return;
        IPullable pull = collision.GetComponent<IPullable>();
        if(pull==null) pull = collision.GetComponentInParent<IPullable>();
        if (pull == null) pull = collision.attachedRigidbody.GetComponent<IPullable>();
        if (pull != null)
        {
            _pulledObject = pull;
            _pulledObject.StartPull(transform);
            
        }
    }
}
