using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS;

public static class PlayerSystem
{
    public static void Init(ref PlayerInfo info, GameObject prefab, int capacity)
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
        info.wishDirections = new Vector3[capacity];
        info.velocities = new Vector3[capacity];
        for (int i = 0; i < capacity; i++)
        {
            info.velocities[i] = Vector3.zero;
        }
        info.baseSpeed = new float[capacity];
        info.bonusSpeed = new float[capacity];
        info.wishFire = new bool[capacity];
        info.fireCooldown = new float[capacity];

        info.health = new int[capacity];
        info.maxHealth = new int[capacity];

        info.upKeys = new KeyCode[capacity];
        info.downKeys = new KeyCode[capacity];
        info.leftKeys = new KeyCode[capacity];
        info.rightKeys = new KeyCode[capacity];
        info.fireKeys = new KeyCode[capacity];

        info.size = 0;
        info.capacity = capacity;
    }

    public static void UpdateWishFire(ref PlayerInfo info)
    {
        for (int i = 0; i < info.size; i++)
        {
            if (Input.GetKeyDown(info.fireKeys[i]))
            {
                info.wishFire[i] = true;
            }
            if (Input.GetKeyUp(info.fireKeys[i]))
            {
                info.wishFire[i] = false;
            }
        }
    }
    public static void UpdateWishDirection(ref PlayerInfo info)
    {
        KeyCode[] keys = info.upKeys;
        for (int i = 0; i < info.size; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                info.wishDirections[i].y += 1.0f;
            }
            if (Input.GetKeyUp(keys[i]))
            {
                info.wishDirections[i].y += -1.0f;
            }
        }
        keys = info.downKeys;
        for (int i = 0; i < info.size; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                info.wishDirections[i].y += -1.0f;
            }
            if (Input.GetKeyUp(keys[i]))
            {
                info.wishDirections[i].y += 1.0f;
            }
        }
        keys = info.leftKeys;
        for (int i = 0; i < info.size; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                info.wishDirections[i].x += -1.0f;
            }
            if (Input.GetKeyUp(keys[i]))
            {
                info.wishDirections[i].x += 1.0f;
            }
        }
        keys = info.rightKeys;
        for (int i = 0; i < info.size; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                info.wishDirections[i].x += 1.0f;
            }
            if (Input.GetKeyUp(keys[i]))
            {
                info.wishDirections[i].x += -1.0f;
            }
        }
    }
}
