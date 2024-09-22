using System.Collections.Generic;

namespace Stellaxis.SiteBlocks.Common;

public interface IDomainEventBuffer
{
    IEnumerable<DomainEvent> DomainEvents { get; }
    
    void AddEvent(DomainEvent eventData);
}