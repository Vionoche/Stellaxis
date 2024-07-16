using System;

namespace ChillSite.SiteBlocks.Common;

public record DomainEvent<TData>(
    Guid EventId,
    DateTime Timestamp,
    TData Data) : IDomainEvent;