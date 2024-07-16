using System.Collections.Generic;

namespace ChillSite.SiteBlocks.Common;

public interface IDomainEventHandler
{
    IEnumerable<IDomainEvent> Events { get; }
    
    void AddEvent<TData>(TData eventData);
}