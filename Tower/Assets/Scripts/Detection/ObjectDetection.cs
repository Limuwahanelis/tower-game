using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectDetection : MonoBehaviour
{
    public UnityEvent OnObjectDetectedUnity;
    public UnityEvent OnObjectLeftdUnity;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnObjectDetectedUnity?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnObjectLeftdUnity?.Invoke();
    }
}
