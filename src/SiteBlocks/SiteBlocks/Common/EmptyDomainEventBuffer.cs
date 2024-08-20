using System;
using System.Collections.Generic;

namespace ChillSite.SiteBlocks.Common;

public class EmptyDomainEventBuffer : IDomainEventBuffer
{
    public IEnumerable<DomainEvent> DomainEvents => Array.Empty<DomainEvent>();

    public void AddEvent(DomainEvent domainEvent)
    {
    }
}