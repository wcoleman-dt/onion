using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetEvents();
        Event GetEvent(byte[] hashedByteValue);
        Event GetEvent(int eventId);
    }
}
