using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAbilities
{
    public enum Abilites
    {
        HARPOON,
    }
    public static bool[] UnlockedAbilities=>_unlockedAbilites;
    private static bool[] _unlockedAbilites=new bool[4];

    public static void UnlockAbility(Abilites ability)
    {
        _unlockedAbilites[((int)ability)] = true;
    }
}
