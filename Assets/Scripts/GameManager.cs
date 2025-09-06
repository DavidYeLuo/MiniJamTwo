using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS;
using Config;

public class GameManger : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerConfig playerConfig;
    private PlayerInfo playerInfo;

    [Space]

    [Header("Bullet")]
    [SerializeField] private BulletConfig basicBulletConfig;
    private BulletInfo bulletInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = new PlayerInfo();
        bulletInfo = new BulletInfo();

        PlayerSystem.Init(ref playerInfo, playerConfig.playerPrefab, playerConfig.maxPlayer);
        BulletSystem.Init(ref bulletInfo, basicBulletConfig.prefab, basicBulletConfig.maxBullets);

        for (int i = 0; i < playerConfig.activePlayer; i++)
        {
            playerInfo.gameObjects[i].SetActive(true);
            playerInfo.baseSpeed[i] = playerConfig.playerBaseSpeed;
            // Bind playerInput to an entity
            playerInfo.upKeys[i] = playerConfig.playerInputs[i].upKey;
            playerInfo.downKeys[i] = playerConfig.playerInputs[i].downKey;
            playerInfo.leftKeys[i] = playerConfig.playerInputs[i].leftKey;
            playerInfo.rightKeys[i] = playerConfig.playerInputs[i].rightKey;
            playerInfo.fireKeys[i] = playerConfig.playerInputs[i].fireKey;
        }

        for (int i = 0; i < bulletInfo.capacity; i++)
        {
            bulletInfo.baseSpeed[i] = basicBulletConfig.baseBulletSpeed;
            bulletInfo.spawnOffsets[i] = basicBulletConfig.bulletSpawnOffset;
            bulletInfo.baseLifeTimes[i] = basicBulletConfig.bulletLifeTime;
        }
        playerInfo.size = playerConfig.activePlayer;
    }

    void FixedUpdate()
    {
        HelperSystem.UpdateFire(playerInfo.positions, bulletInfo.spawnOffsets, playerInfo.wishFire, playerInfo.fireCooldown, playerInfo.size, ref bulletInfo, Time.deltaTime);

        HelperSystem.UpdateVelocity(playerInfo.velocities, playerInfo.wishDirections, playerInfo.baseSpeed, playerInfo.bonusSpeed, playerInfo.size, Time.fixedDeltaTime);
        HelperSystem.UpdateVelocity(bulletInfo.velocities, bulletInfo.wishDirections, bulletInfo.baseSpeed, bulletInfo.bonusSpeed, bulletInfo.size, Time.fixedDeltaTime);
        HelperSystem.UpdatePosition(playerInfo.positions, playerInfo.velocities, playerInfo.size);
        HelperSystem.UpdatePosition(bulletInfo.positions, bulletInfo.velocities, bulletInfo.size);
        HelperSystem.MoveGameObject(playerInfo.gameObjects, playerInfo.positions, playerInfo.velocities, playerInfo.size);
        HelperSystem.MoveGameObject(bulletInfo.gameObjects, bulletInfo.positions, bulletInfo.velocities, bulletInfo.size);

        HelperSystem.UpdateLifeTime(ref bulletInfo, Time.fixedDeltaTime);
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
                fireCooldown[i] = 0.5f; // TODO: Refactor this magical number

                int index = bulletInfo.size;
                GameObject bullet = bulletInfo.gameObjects[index];
                bulletInfo.size++;
                bullet.transform.LookAt(Vector3.up);
                bullet.SetActive(true);
                bulletInfo.positions[index] = playerPosition[i] + bulletSpawnOffset[index];
                bulletInfo.wishDirections[index] = Vector3.up;
                bulletInfo.lifeTimes[index] = bulletInfo.baseLifeTimes[index];
            }
        }
    }
    public static void UpdateLifeTime(ref BulletInfo info, float deltaTime)
    {
        for (int i = 0; i < info.size; i++)
        {
            info.lifeTimes[i] -= deltaTime;
            if (info.lifeTimes[i] < 0.0f)
            {
                info.gameObjects[i].SetActive(false);
                BulletSystem.Moveback(ref info, i);
                info.size--;
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
