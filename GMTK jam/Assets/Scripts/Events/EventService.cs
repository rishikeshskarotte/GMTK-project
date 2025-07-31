using Player;
using UnityEngine;

namespace Events
{
    public class EventService
    {
        public EventController<PlayerController> OnPlayerDiedd;
        
        public EventService()
        {
            OnPlayerDiedd =  new EventController<PlayerController>();
        }

    }
}