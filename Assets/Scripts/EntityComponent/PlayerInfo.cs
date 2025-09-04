using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS;

public struct PlayerInfo
{
    public GameObject[] gameObjects;
    public Array<Entity> entities;
    public Array<Vector3> positions;
    public Array<Vector3> wishDirections;

    public int size; // Active entities
    public int capacity;
}

