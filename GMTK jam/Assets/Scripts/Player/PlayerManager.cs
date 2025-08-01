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
        private Dictionary<PlayerState, Queue<PlayerController>> spawnedPlayers = new();

        private void Start()
        {
            SubscribeToEvents();
            playerPool = new PlayerPool(playerPrefab, playerData, this.transform);
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
            if (!spawnedPlayers.TryGetValue(PlayerState.SkeletonState, out var skeletonQueue) || skeletonQueue.Count == 0)
                return;

            PlayerController skeletonPlayer = skeletonQueue.Dequeue();
            skeletonPlayer.PlayerStateMachine.ChangeState(PlayerState.GhostState);

            if (!spawnedPlayers.ContainsKey(PlayerState.GhostState))
                spawnedPlayers[PlayerState.GhostState] = new Queue<PlayerController>();

            spawnedPlayers[PlayerState.GhostState].Enqueue(skeletonPlayer);
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
