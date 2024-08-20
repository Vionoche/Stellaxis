using System;

namespace ChillSite.SiteBlocks.Common;

public abstract record DomainEventBase : IDomainEvent
{
    public Guid EventId { get; }
    public DateTime OccuredOn { get; }
    
    protected DomainEventBase(Guid eventId, DateTime occuredOn)
    {
        EventId = eventId;
        OccuredOn = occuredOn;
    }
}