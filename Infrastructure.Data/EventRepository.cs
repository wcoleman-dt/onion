using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly MonthlyReportingModel _model;

        public EventRepository()
        {
            _model = new MonthlyReportingModel();
        }

        public Event SaveEvent(Event eventObject)
        {
            if (eventObject != null)
            {
                if (_model.Events.Any(r => !r.HashedByteValue.SequenceEqual(eventObject.HashedByteValue)))
                {
                    _model.Events.Add(eventObject);
                    _model.SaveChanges();
                }
            }
            return eventObject;
        }

        public IEnumerable<Event> GetEvents()
        {
            return _model.Events?.ToList();
        }

        public Event GetEvent(byte[] hashedByteValue)
        {
            Event retVal = null;
            if (hashedByteValue != null)
            {
                retVal = _model.Events.FirstOrDefault(r => r.HashedByteValue.SequenceEqual(hashedByteValue));
            }
            return retVal;
        }

        public Event GetEvent(int eventId)
        {
            Event retVal = null;
            if (eventId > 0)
            {
                retVal = _model.Events.FirstOrDefault(r => r.Id == eventId);
            }
            return retVal;
        }
    }
}
