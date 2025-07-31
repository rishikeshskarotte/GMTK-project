using Player.StateMachine;
using Player.States;
using UnityEngine;

namespace Player
{
    public class PlayerController
    {
        private PlayerView playerView;
        private PlayerModel playerModel;
        private PlayerStateMachine playerStateMachine;
        
        public PlayerView PlayerView => playerView;
        public PlayerModel PlayerModel => playerModel;
        public PlayerStateMachine PlayerStateMachine => playerStateMachine;
        
        public PlayerController(PlayerView playerPrefab, PlayerSO playerData)
        {
            this.playerView = Object.Instantiate(playerPrefab);
            playerModel = new PlayerModel(playerData);
            CreateStateMachine();
        }

        private void CreateStateMachine()
        {
            playerStateMachine = new PlayerStateMachine(this);
            playerStateMachine.ChangeState(PlayerState.AliveState);
        }

        public void EnablePlayer(bool value)
        {
            
        }

        public void SetPlayer(Transform level1Checkpoint)
        {
           
        }
    }
}