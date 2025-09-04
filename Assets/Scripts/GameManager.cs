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
    private PlayerInfo playerInfo;

    [Space]

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int maxBullets;
    private BulletInfo bulletInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = new PlayerInfo();
        bulletInfo = new BulletInfo();

        PlayerSystem.Init(ref playerInfo, playerPrefab, maxPlayer);
        BulletSystem.Init(ref bulletInfo, bulletPrefab, maxBullets);

        bulletInfo.size = maxBullets / 2;
        HelperSystem.RandomizeVectors(ref bulletInfo.positions, bulletInfo.size, new Vector2(-10, 10));
        for (int i = 0; i < bulletInfo.size; i++)
        {
            bulletInfo.gameObjects[i].transform.position = bulletInfo.positions.array[i];
            bulletInfo.gameObjects[i].SetActive(true);
        }
    }

    void FixedUpdate()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
}

public static class HelperSystem
{
    public static void RandomizeVectors(ref Array<Vector3> vectors, int size, Vector2 range)
    {
        for (int i = 0; i < size; i++)
        {
            float x = Random.Range(range.x, range.y);
            float y = Random.Range(range.x, range.y);
            vectors.array[i].x = x;
            vectors.array[i].y = y;
        }
    }
}
