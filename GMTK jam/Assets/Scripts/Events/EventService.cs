using Player;
using Player.Ghost;
using UnityEngine;

namespace Events
{
    public class EventService
    {
        public EventController<PlayerController> OnPlayerDied;
        public EventController OnSkeltonRevived;
        public EventController<Transform> OnSwitchPlaced;
        public EventController<GhostController> OnGhostDestroyed;
        
        public EventService()
        {
            OnPlayerDied =  new EventController<PlayerController>();
            OnSkeltonRevived =  new EventController();
            OnSwitchPlaced =  new EventController<Transform>();
            OnGhostDestroyed =  new EventController<GhostController>();
        }

      
    }
}