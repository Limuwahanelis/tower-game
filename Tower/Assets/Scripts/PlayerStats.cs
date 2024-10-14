using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats 
{
    public static int PlayerHealth => _playerHealth;
    private static int _playerHealth=3;

    public static void SetPlayerHealth(int value)
    {
        _playerHealth = value;
    }
}
