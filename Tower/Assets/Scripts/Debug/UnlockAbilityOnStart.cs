using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbilityOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
        foreach (PlayerAbilities.Abilites val in Enum.GetValues(typeof(PlayerAbilities.Abilites)))
        {
            PlayerAbilities.UnlockAbility(val);
        }
    }
}
