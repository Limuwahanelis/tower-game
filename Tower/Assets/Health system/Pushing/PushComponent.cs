using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PushComponent :MonoBehaviour, IPushable
{
    public UnityEvent<PushInfo> OnPushed;
    [SerializeField] float _pushInvIncibilityTime;
    private bool _canBePushed = true;
    public void Push(PushInfo pushInfo)
    {
        if (!_canBePushed) return;
        _canBePushed = false;
        OnPushed?.Invoke(pushInfo);
        StartCoroutine(PushCor());
    }
    IEnumerator PushCor()
    {
        yield return new WaitForSeconds(_pushInvIncibilityTime);
        _canBePushed = true;
    }
}
