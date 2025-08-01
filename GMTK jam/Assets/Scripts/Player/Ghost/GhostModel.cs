using System.Numerics;
using Vector2 = UnityEngine.Vector2;

namespace Player.Ghost
{
    public class GhostModel
    {
        private GhostSO ghostData;
        
        public GhostSO GhostData => ghostData;
        public Vector2 InputVector;

        public GhostModel(GhostSO ghostData)
        {
            this.ghostData = ghostData;
        }
    }
}