using UnityEngine;

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
            //ghostView.SetController(this);
            ghostModel = new GhostModel(ghostData);
            //SubscribeToEvents();
        }
    }
}