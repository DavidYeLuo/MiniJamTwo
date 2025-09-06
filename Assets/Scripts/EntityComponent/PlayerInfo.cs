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
    public Vector3[] velocities;
    public float[] baseSpeed;
    public float[] bonusSpeed;
    public bool[] wishFire;
    public float[] fireCooldown;

    public int[] health;
    public int[] maxHealth;

    public KeyCode[] upKeys;
    public KeyCode[] downKeys;
    public KeyCode[] leftKeys;
    public KeyCode[] rightKeys;
    public KeyCode[] fireKeys;

    public int size; // Active entities
    public int capacity;
}

