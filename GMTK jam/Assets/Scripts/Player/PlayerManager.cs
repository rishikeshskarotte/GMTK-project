using System;
using System.Collections.Generic;
using Main;
using Player.Ghost;
using Player.States;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Transform level1Checkpoint;
        [SerializeField] private PlayerView playerPrefab;
        [SerializeField] private PlayerSO playerData;
        [SerializeField] private Transform ghostParent;
        [SerializeField] private GhostView ghostPrefab;
        [SerializeField] private GhostSO ghostData;
        
        private PlayerPool playerPool;
        private GhostPool ghostPool;
        
        private Dictionary<PlayerState, Queue<PlayerController>> spawnedPlayers = new();

        private void Start()
        {
            SubscribeToEvents();
            ghostPool = new GhostPool(ghostPrefab, ghostParent, ghostData);
            playerPool = new PlayerPool(playerPrefab, playerData, this.transform, ghostPool);
            SpawnPlayer();
        }

        private void Update()
        {
            foreach (var playerQueue in new List<Queue<PlayerController>>(spawnedPlayers.Values))
            {
                foreach (var player in playerQueue)
                {
                    player.PlayerStateMachine.Tick();
                }
            }
        }

        private void FixedUpdate()
        {
            foreach (var playerQueue in new List<Queue<PlayerController>>(spawnedPlayers.Values))
            {
                foreach (var player in playerQueue)
                {
                    player.PlayerStateMachine.FixedTick();
                }
            }
        }

        private void SubscribeToEvents()
        {
            GameManager.Instance.EventService.OnPlayerDied.AddListener(OnPlayerDied);
            GameManager.Instance.EventService.OnSkeltonRevived.AddListener(OnSkeletonRevived);
        }

        private void OnSkeletonRevived()
        {
            PlayerController alivePlayer;
            
            if (spawnedPlayers.TryGetValue(PlayerState.AliveState, out var aliveQueue) && aliveQueue.Count > 0)
            {
                alivePlayer = aliveQueue.Peek();
                
                if (spawnedPlayers.TryGetValue(PlayerState.SkeletonState, out var skeletonQueue) && aliveQueue.Count > 0)
                {
                    PlayerController skeletonPlayer = skeletonQueue.Peek();
                    alivePlayer.CreateGhost(ghostPool, skeletonPlayer);
                }
                
            }
        }

        private void OnPlayerDied(PlayerController playerController)
        {
            if (spawnedPlayers.ContainsKey(PlayerState.AliveState))
            {
                spawnedPlayers[PlayerState.AliveState].Clear(); 
            }
            if (!spawnedPlayers.ContainsKey(PlayerState.SkeletonState))
                spawnedPlayers[PlayerState.SkeletonState] = new Queue<PlayerController>();

            spawnedPlayers[PlayerState.SkeletonState].Enqueue(playerController);

            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            PlayerController player = playerPool.GetItem();

            if (!spawnedPlayers.ContainsKey(PlayerState.AliveState))
                spawnedPlayers[PlayerState.AliveState] = new Queue<PlayerController>();

            spawnedPlayers[PlayerState.AliveState].Enqueue(player);
            player.SetPlayer(level1Checkpoint);
        }
    }
}
