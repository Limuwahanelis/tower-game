using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthSystem;

public class PlayerHealthSystem : HealthSystem,IPushable
{
    public Action<PushInfo> OnPushed;
    [SerializeField] Ringhandle _pushHandle;
    [SerializeField] float _invincibilityAfterPushDuration;
    private bool _isInvincibleAfterPush;
    private new void Start()
    {
        if (_hpBar == null) return;
        _hpBar.SetMaxHealth(_maxHP);
        _hpBar.SetHealth(_maxHP);
        if(PlayerStats.PlayerHealth<_currentHP)
        {
            _hpBar.SetMaxHealth(PlayerStats.PlayerHealth);
            _hpBar.SetHealth(PlayerStats.PlayerHealth);
        }
    }
    IEnumerator PushCor(Collider2D[] colls)
    {
        _isInvincibleAfterPush = true;
        yield return new WaitForSeconds(_invincibilityAfterPushDuration);
        _isInvincibleAfterPush = false;
        RestoreCollisions(colls);

    }

    public void Push(PushInfo pushInfo)
    {
        if (!IsAlive) return;
        if (_isInvincibleAfterPush) return;
        OnPushed?.Invoke(pushInfo);
        if (pushInfo.involvedColliders != null && pushInfo.involvedColliders.Length>0)
        {
            //PreventCollisions(pushInfo.involvedColliders);
            StartCoroutine(PushCor(pushInfo.involvedColliders));
        }
    }
    public void IncreaseHealthBarMaxValue()
    {
        _hpBar.SetMaxHealth(_maxHP);
        _hpBar.SetHealth(_currentHP);
    }


}
