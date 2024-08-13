using System;
using ChillSite.SiteBlocks.Common;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PageNameUpdatedDomainEvent(
    Guid DomainEventId,
    DateTime Timestamp,
    Guid PageId)
    : DomainEvent(DomainEventId, Timestamp)
{
    public static PageNameUpdatedDomainEvent Create(IDateTimeService dateTimeService, Guid pageId)
    {
        return new PageNameUpdatedDomainEvent(
            DomainEventId: Guid.NewGuid(),
            Timestamp: dateTimeService.UtcNow,
            pageId);
    }
}