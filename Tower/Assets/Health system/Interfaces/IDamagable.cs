﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IDamagable
{
    public delegate void OnDeathEventHandler(IDamagable damagable, DamageInfo info);
    public event OnDeathEventHandler OnDeath;
    void TakeDamage(DamageInfo info);
    void Kill(DamageInfo info);
}
