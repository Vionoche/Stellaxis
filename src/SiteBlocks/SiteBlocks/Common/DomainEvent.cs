using System;

namespace ChillSite.SiteBlocks.Common;

public abstract record DomainEvent(
    Guid EventId,
    DateTime OccuredOn);