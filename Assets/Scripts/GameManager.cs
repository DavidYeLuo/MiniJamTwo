using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS;

public class GameManger : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private int activePlayer;
    [SerializeField] private int maxPlayer;
    [SerializeField] private PlayerInputs[] playerInputs;
    [SerializeField] private float playerBaseSpeed;
    private PlayerInfo playerInfo;

    [Space]

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int maxBullets;
    [SerializeField] private float baseBulletSpeed;
    [SerializeField] private Vector3 bulletSpawnOffset;
    private BulletInfo bulletInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = new PlayerInfo();
        bulletInfo = new BulletInfo();

        PlayerSystem.Init(ref playerInfo, playerPrefab, maxPlayer);
        BulletSystem.Init(ref bulletInfo, bulletPrefab, maxBullets);

        for (int i = 0; i < activePlayer; i++)
        {
            playerInfo.gameObjects[i].SetActive(true);
            playerInfo.baseSpeed[i] = playerBaseSpeed;
            // Bind playerInput to an entity
            playerInfo.upKeys[i] = playerInputs[i].upKey;
            playerInfo.downKeys[i] = playerInputs[i].downKey;
            playerInfo.leftKeys[i] = playerInputs[i].leftKey;
            playerInfo.rightKeys[i] = playerInputs[i].rightKey;
            playerInfo.fireKeys[i] = playerInputs[i].fireKey;
        }

        for (int i = 0; i < bulletInfo.capacity; i++)
        {
            bulletInfo.baseSpeed[i] = baseBulletSpeed;
            bulletInfo.spawnOffsets[i] = bulletSpawnOffset;
        }
        playerInfo.size = activePlayer;
    }

    void FixedUpdate()
    {
        HelperSystem.UpdateFire(playerInfo.positions, bulletInfo.spawnOffsets, playerInfo.wishFire, playerInfo.fireCooldown, playerInfo.size, ref bulletInfo, Time.fixedDeltaTime);

        HelperSystem.UpdateVelocity(playerInfo.velocities, playerInfo.wishDirections, playerInfo.baseSpeed, playerInfo.bonusSpeed, playerInfo.size, Time.fixedDeltaTime);
        HelperSystem.UpdateVelocity(bulletInfo.velocities, bulletInfo.wishDirections, bulletInfo.baseSpeed, bulletInfo.bonusSpeed, bulletInfo.size, Time.fixedDeltaTime);
        HelperSystem.UpdatePosition(playerInfo.positions, playerInfo.velocities, playerInfo.size);
        HelperSystem.UpdatePosition(bulletInfo.positions, bulletInfo.velocities, bulletInfo.size);
        HelperSystem.MoveGameObject(playerInfo.gameObjects, playerInfo.positions, playerInfo.velocities, playerInfo.size);
        HelperSystem.MoveGameObject(bulletInfo.gameObjects, bulletInfo.positions, bulletInfo.velocities, bulletInfo.size);
    }
    // Update is called once per frame
    void Update()
    {
        PlayerSystem.UpdateWishFire(ref playerInfo);
        PlayerSystem.UpdateWishDirection(ref playerInfo);
    }
}

public static class HelperSystem
{
    public static void RandomizeVectors(Vector3[] vectors, int size, Vector2 range)
    {
        for (int i = 0; i < size; i++)
        {
            float x = Random.Range(range.x, range.y);
            float y = Random.Range(range.x, range.y);
            vectors[i].x = x;
            vectors[i].y = y;
        }
    }
    public static void UpdateVelocity(Vector3[] velocities, Vector3[] wishDirections, float[] baseSpeeds, float[] bonusSpeeds, int size, float deltaTime)
    {
        for (int i = 0; i < size; i++)
        {
            float speed = baseSpeeds[i] + bonusSpeeds[i];
            speed *= deltaTime;
            velocities[i] = wishDirections[i].normalized * speed;
        }
    }
    public static void UpdatePosition(Vector3[] positions, Vector3[] velocities, int size)
    {
        for (int i = 0; i < size; i++)
        {
            positions[i] += velocities[i];
        }
    }
    public static void MoveGameObject(GameObject[] gameObjects, Vector3[] positions, Vector3[] velocities, int size)
    {
        for (int i = 0; i < size; i++)
        {
            gameObjects[i].transform.position = positions[i];
        }
    }
    public static void UpdateFire(Vector3[] playerPosition, Vector3[] bulletSpawnOffset, bool[] wishFire, float[] fireCooldown, int size, ref BulletInfo bulletInfo, float deltaTime)
    {
        for (int i = 0; i < size; i++)
        {
            fireCooldown[i] -= deltaTime;
            if (!wishFire[i]) continue; // Don't shoot when user don't want to shoot
            if (fireCooldown[i] < 0.0f)
            {
                fireCooldown[i] += 0.5f; // TODO: Refactor this magical number

                int index = bulletInfo.size;
                GameObject bullet = bulletInfo.gameObjects[index];
                bulletInfo.size++;
                bullet.transform.LookAt(Vector3.up);
                bullet.SetActive(true);
                bulletInfo.positions[index] = playerPosition[i] + bulletSpawnOffset[index];
                bulletInfo.wishDirections[index] = Vector3.up;
            }
        }
    }
}

// Helps with making it show up in the inspector
[System.Serializable]
public struct PlayerInputs
{
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode fireKey;
}
