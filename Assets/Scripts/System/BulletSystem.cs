using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS;

public static class BulletSystem
{
    public static void Init(ref BulletInfo info, GameObject prefab, int capacity)
    {
        info.gameObjects = new GameObject[capacity];
        for (int i = 0; i < capacity; i++)
        {
            GameObject spawn = GameObject.Instantiate(prefab);
            spawn.SetActive(false);
            info.gameObjects[i] = spawn;
        }
        info.entities = new Array<Entity>(capacity);
        info.positions = new Array<Vector3>(capacity);
        info.wishDirections = new Array<Vector3>(capacity);

        info.size = 0;
        info.capacity = capacity;
    }
}
