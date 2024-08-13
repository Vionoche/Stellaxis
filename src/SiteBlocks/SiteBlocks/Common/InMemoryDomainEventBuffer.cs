using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ChillSite.SiteBlocks.Common;

public class InMemoryDomainEventBuffer : IDomainEventBuffer
{
    public InMemoryDomainEventBuffer(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
    }

    public IEnumerable<IDomainEvent> DomainEvents => _events;

    public void AddEvent(IDomainEvent domainEvent)
    {
        _events.Enqueue(domainEvent);
    }

    private readonly IDateTimeService _dateTimeService;
    private readonly ConcurrentQueue<IDomainEvent> _events = [];
}