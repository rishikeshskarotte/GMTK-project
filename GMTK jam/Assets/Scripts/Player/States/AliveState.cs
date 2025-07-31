using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.StateMachine;

namespace Player.States
{
    public class AliveState : IState<PlayerController>
    {
        public PlayerController Owner { get; set; }

        private bool jumpPressed;

        public void OnEnter()
        {
            Owner.PlayerView.SpriteRenderer.color = Color.green;
        }

        public void OnExit() {}

        public void Tick()
        {
            GetInput();
            Move();
            Jump();
        }

        private void GetInput()
        {
            Owner.PlayerModel.moveInput = 0;
            jumpPressed = false;

            if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
                Owner.PlayerModel.moveInput -= 1f;

            if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
                Owner.PlayerModel.moveInput += 1f;

            if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
                jumpPressed = true;
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
            if (jumpPressed && Owner.PlayerModel.IsGrounded)
            {
                Owner.PlayerView.PlayerRB.linearVelocity = new Vector2(
                    Owner.PlayerView.PlayerRB.linearVelocity.x,
                    Owner.PlayerModel.PlayerData.StateDataDict[PlayerState.AliveState].JumpForce
                );
            }
        }
    }
}