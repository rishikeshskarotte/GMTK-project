using UnityEngine;
using Utilities.StateMachine;

namespace Player.States
{
    public class SkeletonState : IState<PlayerController>
    {
        public PlayerController Owner { get; set; }
        
        public void OnEnter()
        {
            Owner.PlayerView.SpriteRenderer.color = Color.blue;
        }

        public void OnExit()
        {
           
        }

        public void Tick()
        {
           
        }

        public void FixedTick()
        {
            
        }
    }
}