using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationsEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent OnStartCheckForEnemyColliders = new UnityEvent();
    public UnityEvent OnStopCheckForEnemyColliders = new UnityEvent();
    public UnityEvent OnWallClimbed = new UnityEvent();
    public void StartCheckForEnemyColliders() => OnStartCheckForEnemyColliders?.Invoke();
    public void StopCheckForEnemyColliders() => OnStopCheckForEnemyColliders?.Invoke();
    public void WallClimbed()=>OnWallClimbed?.Invoke();
}
