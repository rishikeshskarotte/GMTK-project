using Player;
using UnityEngine;

namespace Events
{
    public class EventService
    {
        public EventController<PlayerController> OnPlayerDied;
        public EventController OnSkeltonRevived;
        
        public EventService()
        {
            OnPlayerDied =  new EventController<PlayerController>();
            OnSkeltonRevived =  new EventController();
        }

    }
}