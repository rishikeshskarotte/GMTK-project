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
        
        private void Update()
        {
            foreach (var playerList in new List<List<PlayerController>>(spawnedPlayers.Values))
            {
                foreach (var player in new List<PlayerController>(playerList))
                {
                    player.PlayerStateMachine.Tick();
                }
            }
        }

        private void FixedUpdate()
        {
            foreach (var playerList in new List<List<PlayerController>>(spawnedPlayers.Values))
            {
                foreach (var player in new List<PlayerController>(playerList))
                {
                    player.PlayerStateMachine.FixedTick();
                }
            }
        }

        private void SubscribeToEvents()
        {
            GameManager.Instance.EventService.OnPlayerDied.AddListener(OnplayerDied);
            GameManager.Instance.EventService.OnSkeltonRevived.AddListener(OnSkeltonRevived);
        }

        private void OnSkeltonRevived()
        {
           if(!spawnedPlayers.ContainsKey(PlayerState.SkeletonState)) return;

           if (spawnedPlayers.TryGetValue(PlayerState.SkeletonState, out var skeletonPlayers) 
               && skeletonPlayers.Count > 0)
           {
              skeletonPlayers[0].PlayerStateMachine.ChangeState(PlayerState.GhostState);
              
              if (!spawnedPlayers.ContainsKey(PlayerState.GhostState))
              {
                  spawnedPlayers[PlayerState.GhostState] = new ();
              }
              spawnedPlayers[PlayerState.GhostState].Add( skeletonPlayers[0]);
              
           }
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
    }
}
