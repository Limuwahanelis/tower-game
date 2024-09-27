using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int _dmg;
    protected bool _isCheckingForCollisions;
    protected List<IDamagable> _damagables = new List<IDamagable>();
    protected DamageInfo _damageInfo;
    public void SetCheckForCollisions(bool value)
    {
        _isCheckingForCollisions = value;
        if (value) _damagables.Clear();
    }
    public void ResetTargets()
    {
        _damagables.Clear();
    }
}
