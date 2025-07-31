using Utilities.StateMachine;

namespace Player.States
{
    public class GhostState  : IState<PlayerController>
    {
        public PlayerController Owner { get; set; }
        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}