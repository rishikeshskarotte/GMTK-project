using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        private PlayerSO playerData;
        
        public bool IsGrounded;
        public float moveInput;
        public PlayerSO PlayerData => playerData;
        
        public PlayerModel(PlayerSO playerSo)
        {
           this.playerData = playerSo;
        }
    }
}