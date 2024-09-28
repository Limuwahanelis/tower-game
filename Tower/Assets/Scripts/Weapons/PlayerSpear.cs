using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

public class PlayerSpear : Weapon
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_isCheckingForCollisions) return;

        IDamagable damagable = collision.GetComponent<IDamagable>();
        if (damagable != null)
        {
            _damageInfo = new DamageInfo()
            {
                dmg = _dmg,
                dmgPosition = transform.position,
            };
            if (!_damagables.Contains(damagable))
            {
                _damagables.Add(damagable);
                damagable.TakeDamage(_damageInfo);
            }
        }
    }
}
