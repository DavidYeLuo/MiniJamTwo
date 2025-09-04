using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS;

public struct PlayerInfo
{
    public GameObject[] gameObjects;
    public Entity[] entities;
    public Vector3[] positions;
    public Vector3[] wishDirections;

    public int size; // Active entities
    public int capacity;
}

