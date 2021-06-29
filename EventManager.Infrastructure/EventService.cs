using EventManager.Application.Interfaces;
using EventManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Infrastructure
{
    public class EventService : IDataRepository<Event>
    {
        readonly EventContext _eventContext;
        public EventService(EventContext context)
        {
            _eventContext = context;
        }

        public async Task<Event> Get(Guid id)
        {
            return await _eventContext.Events.FirstOrDefaultAsync(e => e.Identifier == id);
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _eventContext.Events
                .OrderBy(e => e.StartDate)
                .ToListAsync();
        }


        public async Task Add(Event eventEntity)
        {
            _eventContext.Events.Add(eventEntity);
            await _eventContext.SaveChangesAsync();
        }

        public async Task Delete(Event eventEntity)
        {
            _eventContext.Events.Remove(eventEntity);
            await _eventContext.SaveChangesAsync();
        }

        public async Task Update(Event dbEventEntity, Event eventEntity)
        {
            dbEventEntity.EventName = eventEntity.EventName;
            dbEventEntity.EventDescription = eventEntity.EventDescription;
            dbEventEntity.EventTimezone = eventEntity.EventTimezone;
            dbEventEntity.StartDate = eventEntity.StartDate;
            dbEventEntity.EndDate = eventEntity.EndDate;
            dbEventEntity.ModifiedDate =DateTime.Now;

            await _eventContext.SaveChangesAsync();
        }
    }
}
