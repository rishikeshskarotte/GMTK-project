using Main;
using Player.Ghost;
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
        private GhostController ghostController;
        private GhostPool ghostPool;
        
        public PlayerView PlayerView => playerView;
        public PlayerModel PlayerModel => playerModel;
        public PlayerStateMachine PlayerStateMachine => playerStateMachine;
        public GhostController GhostController => ghostController;
        
        public PlayerController(PlayerView playerPrefab, PlayerSO playerData, Transform parentTransform, GhostPool ghostPool = null)
        {
            this.playerView = Object.Instantiate(playerPrefab, parentTransform);
            playerView.SetController(this);
            playerModel = new PlayerModel(playerData);
            this.ghostPool = ghostPool;
            CreateStateMachine();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameManager.Instance.EventService.OnGhostDestroyed.AddListener(onGhostDestroyed);
        }
        
        private void UnsubscribeFromEvents()
        {
            GameManager.Instance.EventService.OnGhostDestroyed.RemoveListener(onGhostDestroyed);
        }
        
        private void onGhostDestroyed(GhostController ghost)
        {
            ghostPool.ReturnItem(ghost);
            ghostController = null;
        }

        public void OnPlayerDied()
        {
            playerStateMachine.ChangeState(PlayerState.SkeletonState);
            GameManager.Instance.EventService.OnPlayerDied.InvokeEvent(this);
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

        public void CreateGhost(GhostPool ghostPool, PlayerController skeletonPlayer)
        {
            // Only set ghostPool if it's not already set
            if (this.ghostPool == null)
            {
                this.ghostPool = ghostPool;
            }
            ghostController = this.ghostPool.GetItem();
            ghostController.SetGhost(skeletonPlayer);
        }
    }
}