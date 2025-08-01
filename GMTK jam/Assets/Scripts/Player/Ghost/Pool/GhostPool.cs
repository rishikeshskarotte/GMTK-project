using Player.Ghost;
using UnityEngine;
using Utilities;

namespace Player
{
    public class GhostPool : GenericObjectPool<GhostController>
    {
        private GhostController ghostController;
        private GhostView ghostPrefab;
        private GhostSO ghostData;
        private Transform parentTransform;
        
        public GhostPool(GhostView ghostPrefab, Transform parentTransform, GhostSO ghostData)
        {
            this.ghostPrefab = ghostPrefab;
            this.parentTransform = parentTransform;
            this.ghostData =  ghostData;
        }
        
        protected override GhostController CreateItem()
        {
            return ghostController = new GhostController(ghostPrefab, ghostData, parentTransform);
        }
        
        protected override void OnGet(GhostController ghost)
        {
         ghost.GhostView.gameObject.SetActive(true);
        }

        protected override void OnRelease(GhostController ghost)
        {
         ghost.GhostView.gameObject.SetActive(false);
        }
    }
}