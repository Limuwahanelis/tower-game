﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerHealthSystem;

public class HealthSystem : MonoBehaviour,IDamagable
{
    public event IDamagable.OnDeathEventHandler OnDeath;
    public Action<DamageInfo> OnHitEvent;
    public bool IsAlive => _currentHP > 0;
    public int CurrentHP => _currentHP;
    public int MaxHP => _maxHP;

    [SerializeField] protected Collider2D[] _colliders;
    [SerializeField] protected bool _isInvincible;
    [SerializeField] protected HealthBar _hpBar;
    [SerializeField] protected int _maxHP;
    [SerializeField] protected int _currentHP;
    [SerializeField] float _invincibilityAfterHitDuration;
    protected bool _isInvincibleToDamage = false;




    // Start is called before the first frame update
    protected void Start()
    {
        if (_hpBar != null)
        {
            _hpBar.SetMaxHealth(_maxHP);
            _hpBar.SetHealth(_maxHP);
        }
        _currentHP = _maxHP;
    }
    public virtual void TakeDamage(DamageInfo info)
    {
        if (_isInvincibleToDamage) return;
        if (!IsAlive) return;
        _currentHP -= info.dmg;
        if (_hpBar != null) _hpBar.SetHealth(_currentHP);
        OnHitEvent?.Invoke(info);
        StartCoroutine(DamageInvincibilityCor());
        if (_currentHP <= 0) Kill(info);
    }
    /// <summary>
    /// Deals damage wihtout rasing any events.
    /// </summary>
    /// <param name="info"></param>
    public virtual void TakeDamageWithoutNotify(DamageInfo info)
    {
        if (!IsAlive) return;
        _currentHP -= info.dmg;
        _hpBar.SetHealth(_currentHP);
        if (_currentHP <= 0) Kill(info);
    }
    protected bool IsDeathEventSubscribedTo()
    {
        if (OnDeath == null) return false;
         return true;
    }
    protected void InvokeOnDeathEvent(DamageInfo info)
    {
        OnDeath?.Invoke(this,info);
    }
    public virtual void Kill(DamageInfo info)
    {
        if (!IsDeathEventSubscribedTo())
        {
            Destroy(gameObject);
            if(_hpBar!=null) Destroy(_hpBar.gameObject);
        }
        else OnDeath?.Invoke(this,info);
    }
    IEnumerator DamageInvincibilityCor()
    {
        _isInvincibleToDamage = true;
        yield return new WaitForSeconds(_invincibilityAfterHitDuration);
        _isInvincibleToDamage = false;
    }

    #region collisions
    protected void PreventCollisions(Collider2D[] collidersToPreventCollisionsFrom)
    {
        foreach(Collider2D collider in collidersToPreventCollisionsFrom)
        {
            foreach(Collider2D myCol in _colliders)
            {
                Physics2D.IgnoreCollision(collider, myCol, true);
            }
        }
    }

    protected void RestoreCollisions(Collider2D[] collidersToRestoreCollisionsFrom)
    {
        foreach (Collider2D collider in collidersToRestoreCollisionsFrom)
        {
            foreach (Collider2D myCol in _colliders)
            {
                Physics2D.IgnoreCollision(collider, myCol, false);
            }
        }
    }
    #endregion
}
