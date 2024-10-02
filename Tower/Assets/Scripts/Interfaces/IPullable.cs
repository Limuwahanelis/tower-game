using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPullable 
{
    void StartPull(Transform pullerPos);
    void EndPull();
}
