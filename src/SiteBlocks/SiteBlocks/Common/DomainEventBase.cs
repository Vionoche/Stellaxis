using System;

namespace ChillSite.SiteBlocks.Common;

public abstract record DomainEventBase : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccuredOn { get; }

    protected DomainEventBase(DateTime timestamp)
    {
        OccuredOn = timestamp;
    }
}