using System;
using ChillSite.SiteBlocks.Common;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PageContentBlocksDomainUpdated(
    Guid DomainEventId,
    DateTime Timestamp,
    Guid PageId)
    : DomainEvent(DomainEventId, Timestamp)
{
    public static PageContentBlocksDomainUpdated Create(IDateTimeService dateTimeService, Guid pageId)
    {
        return new PageContentBlocksDomainUpdated(
            DomainEventId: Guid.NewGuid(),
            Timestamp: dateTimeService.UtcNow,
            pageId);
    }
}