using System.Collections.Generic;

namespace ChillSite.SiteBlocks.Common;

public interface IDomainEventBuffer
{
    IEnumerable<IDomainEvent> DomainEvents { get; }
    
    void AddEvent(IDomainEvent eventData);
}