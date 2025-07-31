using System;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Transform level1Checkpoint;
        [SerializeField] private PlayerView playerPrefab;
        [SerializeField] private PlayerSO playerData;
        private PlayerPool playerPool;
        
        private void Awake()
        {
            playerPool = new PlayerPool(playerPrefab, playerData);
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            PlayerController player = playerPool.GetItem();
            player.SetPlayer(level1Checkpoint);
        }
    }
}