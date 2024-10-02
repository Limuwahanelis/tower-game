using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpearHarpoon : MonoBehaviour
{
    public Action OnHit;
    [SerializeField] Transform _player;
    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPullable pull = collision.GetComponent<IPullable>();
        if (pull!=null)
        {
            pull.StartPull(transform);
        }
    }
}
