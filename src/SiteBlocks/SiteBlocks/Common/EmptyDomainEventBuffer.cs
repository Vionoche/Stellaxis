using System;
using System.Collections.Generic;

namespace ChillSite.SiteBlocks.Common;

public class EmptyDomainEventBuffer : IDomainEventBuffer
{
    public IEnumerable<IDomainEvent> DomainEvents => Array.Empty<IDomainEvent>();

    public void AddEvent(IDomainEvent domainEvent)
    {
    }
}