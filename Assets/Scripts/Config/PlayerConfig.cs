using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/Entity/Player")]
    public class PlayerConfig : ScriptableObject
    {
        public GameObject playerPrefab;
        public int activePlayer;
        public int maxPlayer;
        public PlayerInputs[] playerInputs;
        public float playerBaseSpeed;
    }
}
