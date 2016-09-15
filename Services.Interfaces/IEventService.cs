using System.Collections.Generic;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IEventService
    {
        IEnumerable<Event> GetEvents();
        Event GetEvent(int eventId);
        Event GetEvent(byte[] hashedByteValue);
    }
}