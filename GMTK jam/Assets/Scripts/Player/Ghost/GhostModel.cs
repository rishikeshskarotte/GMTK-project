namespace Player.Ghost
{
    public class GhostModel
    {
        private GhostSO ghostData;
        
        public GhostSO GhostData => ghostData;
        
        public GhostModel(GhostSO ghostData)
        {
            this.ghostData = ghostData;
        }
    }
}