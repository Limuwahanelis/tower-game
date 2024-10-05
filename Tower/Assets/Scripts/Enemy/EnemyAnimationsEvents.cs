using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimationsEvents : MonoBehaviour
{
    public UnityEvent OnStartCheckForColliders = new UnityEvent();
    public UnityEvent OnStopCheckForColliders = new UnityEvent();
    public void StartCheckColliders() => OnStartCheckForColliders?.Invoke();
    public void StopCheckForColliders() => OnStopCheckForColliders?.Invoke();
}
