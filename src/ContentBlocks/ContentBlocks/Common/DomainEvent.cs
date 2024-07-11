using System;

namespace ChillSite.ContentBlocks.Common;

public record DomainEvent<TData>(
    Guid EventId,
    DateTime Timestamp,
    TData Data) : IDomainEvent;