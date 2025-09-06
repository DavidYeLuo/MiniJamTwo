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
        info.entities = new Entity[capacity];

        info.positions = new Vector3[capacity];
        info.spawnOffsets = new Vector3[capacity];
        info.wishDirections = new Vector3[capacity];
        info.velocities = new Vector3[capacity];
        info.baseSpeed = new float[capacity];
        info.bonusSpeed = new float[capacity];

        info.baseLifeTimes = new float[capacity];
        info.lifeTimes = new float[capacity];

        info.size = 0;
        info.capacity = capacity;
    }
    // Replace element at index with the last element
    // NOTE: This doesn't swap the data
    public static void Moveback(ref BulletInfo info, int index)
    {
        int last = info.size - 1;
        GameObject temp = info.gameObjects[index];
        info.gameObjects[index] = info.gameObjects[last];
        info.gameObjects[last] = temp;
        info.positions[index] = info.positions[last];
        info.spawnOffsets[index] = info.spawnOffsets[last];
        info.wishDirections[index] = info.wishDirections[last];
        info.velocities[index] = info.velocities[last];
        info.baseSpeed[index] = info.baseSpeed[last];
        info.bonusSpeed[index] = info.bonusSpeed[last];
        info.lifeTimes[index] = info.lifeTimes[last];
        info.baseLifeTimes[index] = info.baseLifeTimes[last];
    }
}
