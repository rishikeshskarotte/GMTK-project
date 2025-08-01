using Main;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Ghost
{
    public class GhostController
    {
        private GhostView ghostView;
        private GhostModel ghostModel;
        
        public GhostView GhostView => ghostView;
        public GhostModel GhostModel => ghostModel;

        public GhostController(GhostView ghostPrefab, GhostSO ghostData, Transform parentTransform)
        {
            this.ghostView = Object.Instantiate(ghostPrefab, parentTransform);
            ghostView.SetController(this);
            ghostModel = new GhostModel(ghostData);
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameManager.Instance.EventService.OnSwitchPlaced.AddListener(OnSwitchPlaced);
        }
        
        private void UnsubscribeFromEvents()
        {
            GameManager.Instance.EventService.OnSwitchPlaced.RemoveListener(OnSwitchPlaced);            
        }
        
        private void OnSwitchPlaced(Transform alivePlayerPosition)
        {
           alivePlayerPosition.transform.position = ghostView.transform.position;
           GameManager.Instance.EventService.OnGhostDestroyed.InvokeEvent(this);
        }

        public void GetInput()
        {
           GhostModel.InputVector = Vector2.zero;

            if (Keyboard.current.aKey.isPressed)
                GhostModel.InputVector.x -= 1f;

            if (Keyboard.current.dKey.isPressed)
                GhostModel.InputVector.x += 1f;

            if (Keyboard.current.wKey.isPressed)
                GhostModel.InputVector.y += 1f;

            if (Keyboard.current.sKey.isPressed)
                GhostModel.InputVector.y -= 1f;

            if (Mathf.Approximately(GhostModel.InputVector.x, 1))
            {
                ghostView.transform.localScale = Vector3.one;
            }
            else if (Mathf.Approximately(GhostModel.InputVector.x, -1))
            {
                Vector3 scale = ghostView.transform.localScale;
                scale.x = -1;
                ghostView.transform.localScale = scale;
            }

            GhostModel.InputVector = GhostModel.InputVector.normalized;
        }

        public void Move()
        {
           ghostView.GhostRB.MovePosition(ghostView.GhostRB.position + ghostModel.InputVector * 
                (ghostModel.GhostData.Speed * Time.fixedDeltaTime));
        }

        public void SetGhost(PlayerController skeletonPlayer)
        {
            ghostView.gameObject.SetActive(true);
            RestrictGhost(skeletonPlayer);
        }

        private void RestrictGhost(PlayerController skeletonPlayer)
        {
            ghostView.transform.position = skeletonPlayer.PlayerView.transform.position;
        }
    }
}