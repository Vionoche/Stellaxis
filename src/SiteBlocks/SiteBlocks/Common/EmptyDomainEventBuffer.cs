using System;
using System.Collections.Generic;

namespace Stellaxis.SiteBlocks.Common;

public class EmptyDomainEventBuffer : IDomainEventBuffer
{
    public IEnumerable<DomainEvent> DomainEvents => Array.Empty<DomainEvent>();

    public void AddEvent(DomainEvent domainEvent)
    {
    }
}