using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SImpleEnemyController : MonoBehaviour,IPullable
{
    private Transform _puller; 
    private bool _isBeingPulled = false;
    private Vector3 _pullOffset;
    public void EndPull()
    {
        _isBeingPulled = false;
    }

    public void StartPull(Transform pullerPos)
    {
        _puller = pullerPos;
        _pullOffset= _puller.position-transform.position;
        _isBeingPulled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isBeingPulled)
        {
            transform.position=_puller.position-_pullOffset;
        }
    }

}
