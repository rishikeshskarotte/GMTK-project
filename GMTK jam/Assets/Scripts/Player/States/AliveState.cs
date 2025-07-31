using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.StateMachine;

namespace Player.States
{
    public class AliveState : IState<PlayerController>
    {
        public PlayerController Owner { get; set; }
        
        public void OnEnter()
        {
           
        }

        public void OnExit()
        {
           
        }

        public void Tick()
        {
            Owner.PlayerModel.moveInput = 0;
            
            if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            {
                Owner.PlayerModel.moveInput -= 1f;
            }

            if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            {
                Owner.PlayerModel.moveInput += 1f;
            }
            Move();
        }

        private void Move()
        {
            Owner.PlayerView.transform.position +=
               (Vector3)new Vector2(Owner.PlayerModel.moveInput * Owner.PlayerModel.PlayerData.PlayerSpeed, 0f) * 
               Time.deltaTime;
        }
    }
}