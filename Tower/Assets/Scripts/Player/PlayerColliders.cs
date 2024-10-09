using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliders : MonoBehaviour
{
    [SerializeField]List<Collider2D> _collisionColliders;
    public void SetCollisionsColliders(bool value)
    {
        foreach(Collider2D col in _collisionColliders)
        {
            col.enabled = value;
        }
    }
}
