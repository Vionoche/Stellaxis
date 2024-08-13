using System;
using ChillSite.SiteBlocks.Common;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PagePublishedDomainEvent(
    Guid DomainEventId,
    DateTime Timestamp,
    Guid PageId)
    : DomainEvent(DomainEventId, Timestamp)
{
    public static PagePublishedDomainEvent Create(IDateTimeService dateTimeService, Guid pageId)
    {
        return new PagePublishedDomainEvent(
            DomainEventId: Guid.NewGuid(),
            Timestamp: dateTimeService.UtcNow,
            pageId);
    }
}