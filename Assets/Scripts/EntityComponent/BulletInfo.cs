using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS;

public struct BulletInfo
{
    public GameObject[] gameObjects;
    public Array<Entity> entities;
    public Array<Vector3> positions;
    public Array<Vector3> wishDirections;

    public int size;
    public int capacity;
}
