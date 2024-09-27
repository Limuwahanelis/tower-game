using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class HelperMethods
{

    public static IEnumerator DelyedFunction(float timeToWait, Action function)
    {
        yield return new WaitForSeconds(timeToWait);
        function();
    }
    public static IEnumerator WaitFrame(Action function)
    {
        yield return new WaitForNextFrameUnit();
        function();
    }
}
