using UnityEngine;
using Utilities.StateMachine;

namespace Player.States
{
    public class GhostState  : IState<PlayerController>
    {
        public PlayerController Owner { get; set; }
        public void OnEnter()
        {
            Owner.PlayerView.SpriteRenderer.color = Color.white;
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
        }
    }
}