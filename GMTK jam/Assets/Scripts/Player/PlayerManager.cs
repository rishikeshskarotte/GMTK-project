using System;
using System.Collections.Generic;
using Main;
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
        private Dictionary<PlayerState, List<PlayerController>> spawnedPlayers = new();
        
        private void Start()
        {
            SubscribeToEvents();
            playerPool = new PlayerPool(playerPrefab, playerData, this.transform);
            SpawnPlayer();
        }

        private void SubscribeToEvents()
        {
            GameManager.Instance.EventService.OnPlayerDiedd.AddListener(OnplayerDied);
        }

        private void OnplayerDied(PlayerController playerController)
        {
            if (spawnedPlayers.ContainsKey(PlayerState.AliveState))
            {
                spawnedPlayers[PlayerState.AliveState].Remove(playerController);
            }
            
            if (!spawnedPlayers.ContainsKey(PlayerState.SkeletonState))
            {
                spawnedPlayers[PlayerState.SkeletonState] = new();
            }

            spawnedPlayers[PlayerState.SkeletonState].Add(playerController);
            
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            PlayerController player = playerPool.GetItem();
            
            if (!spawnedPlayers.ContainsKey(PlayerState.AliveState))
            {
                spawnedPlayers[PlayerState.AliveState] = new ();
            }

            spawnedPlayers[PlayerState.AliveState].Add(player);
            player.SetPlayer(level1Checkpoint);
        }

        private void Update()
        {
            foreach (var playerList in spawnedPlayers.Values)
            {
                foreach (var player in playerList)
                {
                    player.PlayerStateMachine.Tick();
                }
            }
        }
    }
}
