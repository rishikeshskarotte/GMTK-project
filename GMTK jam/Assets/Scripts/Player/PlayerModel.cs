using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        private PlayerSO playerData;
        
        public bool IsGrounded;
        public float moveInput;
        public Vector2 InputVector;
        
        public PlayerSO PlayerData => playerData;
        public bool actionTriggered = false;
        public float RevivalButtonHoldTime = 0f;
        public bool JumpPressed = false;
        
        public PlayerModel(PlayerSO playerSo)
        {
           this.playerData = playerSo;
        }
    }
}