using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS;

public struct BulletInfo
{
    public GameObject[] gameObjects;
    public Entity[] entities;

    public Vector3[] positions;
    public Vector3[] spawnOffsets;
    public Vector3[] wishDirections;
    public Vector3[] velocities;
    public float[] baseSpeed;
    public float[] bonusSpeed;

    public float[] lifeTimes;
    public float[] baseLifeTimes;

    public int size;
    public int capacity;
}
