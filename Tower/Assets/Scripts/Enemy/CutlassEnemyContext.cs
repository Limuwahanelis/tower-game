using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutlassEnemyContext : EnemyContext
{
    public ObjectDetection playerFrontDetection;
    public ObjectDetection playerChaseDetection;
    public ObjectDetection playerChaseStopDetection;
    public Vector3 spawnPoint;
    public bool attackPlayer = false;
    public bool chasePlayer = false;
}
