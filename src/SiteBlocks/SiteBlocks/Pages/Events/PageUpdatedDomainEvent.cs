using System;
using ChillSite.SiteBlocks.Common;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PageUpdatedDomainEvent(
    Guid DomainEventId,
    DateTime Timestamp,
    Guid PageId)
    : DomainEvent(DomainEventId, Timestamp)
{
    public static PageUpdatedDomainEvent Create(IDateTimeService dateTimeService, Guid pageId)
    {
        return new PageUpdatedDomainEvent(
            DomainEventId: Guid.NewGuid(),
            Timestamp: dateTimeService.UtcNow,
            pageId);
    }
}