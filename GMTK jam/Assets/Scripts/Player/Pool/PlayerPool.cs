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
        
        public PlayerPool(PlayerView playerPrefab, PlayerSO playerData)
        {
            this.playerPrefab = playerPrefab;
            this.playerData = playerData;
        }
        
        protected override PlayerController CreateItem()
        {
            return playerController = new PlayerController(playerPrefab, playerData);
        }

        protected override void OnGet(PlayerController player)
        {
            player.EnablePlayer(true);
        }

        protected override void OnRelease(PlayerController player)
        {
            player.EnablePlayer(false);
        }
    }
}