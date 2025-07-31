using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.StateMachine;

namespace Player.States
{
    public class GhostState : IState<PlayerController>
    {
        public PlayerController Owner { get; set; }

        public void OnEnter()
        {
            Owner.PlayerView.SpriteRenderer.color = Color.white;
            
            Owner.PlayerView.PlayerRB.linearVelocity = Vector2.zero;
            Owner.PlayerView.PlayerRB.gravityScale = 0f;
            Owner.PlayerView.PlayerRB.bodyType = RigidbodyType2D.Kinematic;
            
        }

        public void OnExit()
        {
            Owner.PlayerView.PlayerRB.gravityScale = 1f;
            Owner.PlayerView.PlayerRB.bodyType = RigidbodyType2D.Dynamic;
        }

        public void Tick()
        {
            GetInput();
        }

        public void FixedTick()
        {
            Move();
        }

        private void GetInput()
        {

                Owner.PlayerModel.InputVector = Vector2.zero;

                if (Keyboard.current.aKey.isPressed)
                    Owner.PlayerModel.InputVector.x -= 1f;

                if (Keyboard.current.dKey.isPressed)
                    Owner.PlayerModel.InputVector.x += 1f;

                if (Keyboard.current.wKey.isPressed)
                    Owner.PlayerModel.InputVector.y += 1f;

                if (Keyboard.current.sKey.isPressed)
                    Owner.PlayerModel.InputVector.y -= 1f;

                Owner.PlayerModel.InputVector = Owner.PlayerModel.InputVector.normalized;
        }

        private void Move()
        {
            Owner.PlayerView.PlayerRB.MovePosition(Owner.PlayerView.PlayerRB.position + Owner.PlayerModel.InputVector * 
                (Owner.PlayerModel.PlayerData.StateDataDict[PlayerState.GhostState].Speed * Time.fixedDeltaTime));
        }
    }
}