using System;
using UnityEngine;
using UnityEngine.Pool;
using Utilities;

namespace Player
{
    public class PlayerPool : GenericObjectPool<PlayerController>
    {
        private PlayerController playerController;
        private PlayerView playerPrefab;
        private PlayerSO playerData;
        private Transform parentTransform;
        private GhostPool ghostPool;
        
        public PlayerPool(PlayerView playerPrefab, PlayerSO playerData, Transform transform, GhostPool ghostPool = null)
        {
            this.playerPrefab = playerPrefab;
            this.playerData = playerData;
            this.parentTransform = transform;
            this.ghostPool = ghostPool;
        }
        
        protected override PlayerController CreateItem()
        {
            return playerController = new PlayerController(playerPrefab, playerData, parentTransform, ghostPool);
        }

        protected override void OnGet(PlayerController player)
        {
            player.PlayerView.gameObject.SetActive(true);
        }

        protected override void OnRelease(PlayerController player)
        {
            player.PlayerView.gameObject.SetActive(false);

        }
    }
}