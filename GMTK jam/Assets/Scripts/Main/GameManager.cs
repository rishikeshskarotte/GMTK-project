using Events;
using Utilities;

namespace Main
{
    public class GameManager : GenericMonoSingelton<GameManager>
    {
        private EventService eventService;
        
        public EventService EventService => eventService;
        
        protected override void Awake()
        {
            base.Awake();
            eventService = new EventService();    
        }
    }
}