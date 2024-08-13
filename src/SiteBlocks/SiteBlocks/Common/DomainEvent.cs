using System;

namespace ChillSite.SiteBlocks.Common;

public abstract record DomainEvent(
    Guid DomainEventId,
    DateTime Timestamp)
    : IDomainEvent;