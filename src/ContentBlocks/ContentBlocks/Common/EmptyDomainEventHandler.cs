using System;
using System.Collections.Generic;

namespace ChillSite.ContentBlocks.Common;

public class EmptyDomainEventHandler : IDomainEventHandler
{
    public IEnumerable<IDomainEvent> Events => Array.Empty<IDomainEvent>();

    public void AddEvent<TData>(TData eventData)
    {
    }
}