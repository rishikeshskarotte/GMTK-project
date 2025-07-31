using System;
using System.Collections.Generic;
using Player.States;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Transform level1Checkpoint;
        [SerializeField] private PlayerView playerPrefab;
        [SerializeField] private PlayerSO playerData;
        
        private PlayerPool playerPool;
        private Dictionary<PlayerState, PlayerController> spawnedPlayers = new();
        
        private void Awake()
        {
            playerPool = new PlayerPool(playerPrefab, playerData);
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            PlayerController player = playerPool.GetItem();
            spawnedPlayers.Add((PlayerState)player.PlayerStateMachine.CurrentStateKey, player);
            player.SetPlayer(level1Checkpoint);
        }

        private void Update()
        {
            foreach (PlayerController player in spawnedPlayers.Values)
            {
                player.PlayerStateMachine.Tick();
            }
        }
    }
}