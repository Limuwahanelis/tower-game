using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlassCombat : MonoBehaviour
{
    [SerializeField] Weapon _cutlass;

    public void StartCheckingForColliders()
    {
        _cutlass.SetCheckForCollisions(true);
    }
    public void StopCheckingForCollisions()
    {
        _cutlass.SetCheckForCollisions(false);
    }
}
