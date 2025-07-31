using UnityEngine;

namespace Player
{
    public class PlayerController
    {
        private PlayerView playerView;
        private PlayerModel playerModel;
        
        public PlayerView PlayerView => playerView;
        public PlayerModel PlayerModel => playerModel;
        
        public PlayerController(PlayerView playerPrefab, PlayerSO playerData)
        {
            this.playerView = Object.Instantiate(playerPrefab);
            playerModel = new PlayerModel(playerData);
        }

        public void EnablePlayer(bool value)
        {
            
        }

        public void SetPlayer(Transform level1Checkpoint)
        {
           
        }
    }
}