using System;
using UnityEngine.Pool;
using Utilities;

namespace Player
{
    public class PlayerPool : GenericObjectPool<PlayerController>
    {
        private PlayerController playerController;
        
        public PlayerPool()
        {

        }
        
        protected override PlayerController CreateItem()
        {
            return playerController = new PlayerController();
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