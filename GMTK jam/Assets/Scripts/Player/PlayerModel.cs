namespace Player
{
    public class PlayerModel
    {
        private PlayerSO playerData;
        
        public PlayerSO PlayerData => playerData;
        
        public PlayerModel(PlayerSO playerSo)
        {
           this.playerData = playerSo;
        }
    }
}