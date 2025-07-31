using Main;
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
            Owner.PlayerView.SpriteRenderer.color = Color.green;
            Owner.PlayerModel.RevivalButtonHoldTime = 0f;
            Owner.PlayerModel.actionTriggered = false;
        }

        public void OnExit() {}

        public void Tick()
        {
            GetInput();
            Move();
            Jump();
        }

        public void FixedTick()
        {
            
        }

        private void GetInput()
        {
            Owner.PlayerModel.moveInput = 0;
            Owner.PlayerModel.JumpPressed = false;

            if (Keyboard.current.leftArrowKey.isPressed)
                Owner.PlayerModel.moveInput -= 1f;

            if (Keyboard.current.rightArrowKey.isPressed )
                Owner.PlayerModel.moveInput += 1f;

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
                Owner.PlayerModel.JumpPressed = true;
            
            if (Keyboard.current.gKey.isPressed)
            {
                Owner.PlayerModel.RevivalButtonHoldTime += Time.deltaTime;

                if (!Owner.PlayerModel.actionTriggered && Owner.PlayerModel.RevivalButtonHoldTime >= Owner.PlayerModel.PlayerData.RevivalTime)
                {
                    ReviveToGhost();  
                    Owner.PlayerModel.actionTriggered = true;
                }
            }
            else
            {
                Owner.PlayerModel.RevivalButtonHoldTime = 0f;
                Owner.PlayerModel.actionTriggered = false;
            }
        }

        private void Move()
        {
            Vector3 move = new Vector2(
                Owner.PlayerModel.moveInput * Owner.PlayerModel.PlayerData.StateDataDict[PlayerState.AliveState].Speed,
                0f
            );
            Owner.PlayerView.transform.position += move * Time.deltaTime;
        }

        private void Jump()
        {
            if (Owner.PlayerModel.JumpPressed && Owner.PlayerModel.IsGrounded)
            {
                Owner.PlayerView.PlayerRB.linearVelocity = new Vector2(
                    Owner.PlayerView.PlayerRB.linearVelocity.x,
                    Owner.PlayerModel.PlayerData.StateDataDict[PlayerState.AliveState].JumpForce
                );
            }
        }

        private void ReviveToGhost()
        {
           GameManager.Instance.EventService.OnSkeltonRevived.InvokeEvent();
        }
    }
}
