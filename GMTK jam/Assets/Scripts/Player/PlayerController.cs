using Main;
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
        
        public PlayerController(PlayerView playerPrefab, PlayerSO playerData, Transform parentTransform)
        {
            this.playerView = Object.Instantiate(playerPrefab, parentTransform);
            playerView.SetController(this);
            playerModel = new PlayerModel(playerData);
            CreateStateMachine();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
        }

        private void UnsubscribeFromEvents()
        {
        }

        public void OnPlayerDied()
        {
            playerStateMachine.ChangeState(PlayerState.SkeletonState);
            GameManager.Instance.EventService.OnPlayerDiedd.InvokeEvent(this);
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