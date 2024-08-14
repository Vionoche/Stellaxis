using System;

namespace ChillSite.SiteBlocks.Common;

public interface IDomainEvent
{
    Guid EventId { get; }
    DateTime OccuredOn { get; }
}