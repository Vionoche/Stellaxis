using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ChillSite.ContentBlocks.Common;

public class MemoryDomainEventHandler : IDomainEventHandler
{
    public MemoryDomainEventHandler(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
    }

    public IEnumerable<IDomainEvent> Events => _events;

    public void AddEvent<TData>(TData eventData)
    {
        var @event = new DomainEvent<TData>(
            EventId: Guid.NewGuid(),
            Timestamp: _dateTimeService.UtcNow,
            eventData);

        _events.Enqueue(@event);
    }

    private readonly IDateTimeService _dateTimeService;
    private readonly ConcurrentQueue<IDomainEvent> _events = [];
}