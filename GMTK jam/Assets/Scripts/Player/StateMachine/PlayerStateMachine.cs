using Player.States;
using Utilities.StateMachine;

namespace Player.StateMachine
{
    public class PlayerStateMachine :GenericStateMachine<PlayerController>
    {
        public PlayerStateMachine(PlayerController owner) : base(owner)
        {
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
          AddState(PlayerState.AliveState, new AliveState());
          AddState(PlayerState.SkeletonState, new SkeletonState());
        }
    }
}