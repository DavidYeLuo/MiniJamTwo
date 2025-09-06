using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Config/Entity/Bullet")]
    public class BulletConfig : ScriptableObject
    {
        public GameObject prefab;
        public int maxBullets;
        public float baseBulletSpeed;
        public Vector3 bulletSpawnOffset;
    }
}
