using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ChillSite.SiteBlocks.Common;

public class InMemoryDomainEventBuffer : IDomainEventBuffer
{
    public InMemoryDomainEventBuffer(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public IEnumerable<IDomainEvent> DomainEvents => _events;

    public void AddEvent(IDomainEvent domainEvent)
    {
        _events.Enqueue(domainEvent);
    }

    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ConcurrentQueue<IDomainEvent> _events = [];
}