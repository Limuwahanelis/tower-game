using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearChain : MonoBehaviour
{
    [SerializeField] float _distanceToShowNewChainSegment;
    [SerializeField] Transform _chainStart;
    [SerializeField] List<GameObject> _chainSegments;
    private int _segments = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {

    }
    public void CheckSegmentsForward()
    {
        transform.up = (transform.position - _chainStart.position).normalized;
        if (Vector2.Distance(_chainStart.position, transform.position) > _distanceToShowNewChainSegment * (_segments+1))
        {
            if (_segments < _chainSegments.Count)
            {
                _chainSegments[_segments].SetActive(true);
                _segments++;
            }
        }
    }
    public void CheckSegmentsback()
    {
        transform.up = (transform.position - _chainStart.position).normalized;
        if (Vector2.Distance(_chainStart.position, transform.position) < _distanceToShowNewChainSegment * (_segments))
        {
            if (_segments >0 )
            {
                _segments--;
                _chainSegments[_segments].SetActive(false);
                
            }
        }
    }
    public void ResetChain()
    {
        for (int i = 0; i <_chainSegments.Count;i++)
        {
            _chainSegments[i].SetActive(false);
        }
        _segments = 0;
    }
}
