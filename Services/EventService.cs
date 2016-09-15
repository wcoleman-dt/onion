using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services.NewRelic
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IEnumerable<Event> GetEvents()
        {
            return _eventRepository.GetEvents();
        }

        public Event GetEvent(int eventId)
        {
            return _eventRepository.GetEvent(eventId);
        }

        public Event GetEvent(byte[] hashedByteValue)
        {
            return _eventRepository.GetEvent(hashedByteValue);
        }
    }
}