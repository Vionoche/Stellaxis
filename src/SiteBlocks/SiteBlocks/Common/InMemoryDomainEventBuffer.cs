using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Stellaxis.SiteBlocks.Common;

public class InMemoryDomainEventBuffer : IDomainEventBuffer
{
    public InMemoryDomainEventBuffer(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public IEnumerable<DomainEvent> DomainEvents => _events;

    public void AddEvent(DomainEvent domainEvent)
    {
        _events.Enqueue(domainEvent);
    }

    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ConcurrentQueue<DomainEvent> _events = [];
}