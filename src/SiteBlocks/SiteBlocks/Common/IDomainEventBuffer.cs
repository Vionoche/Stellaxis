using System.Collections.Generic;

namespace ChillSite.SiteBlocks.Common;

public interface IDomainEventBuffer
{
    IEnumerable<DomainEvent> DomainEvents { get; }
    
    void AddEvent(DomainEvent eventData);
}