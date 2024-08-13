using System;
using ChillSite.SiteBlocks.Common;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PageUnpublishedDomainEvent(
    Guid DomainEventId,
    DateTime Timestamp,
    Guid PageId)
    : DomainEvent(DomainEventId, Timestamp)
{
    public static PageUnpublishedDomainEvent Create(IDateTimeService dateTimeService, Guid pageId)
    {
        return new PageUnpublishedDomainEvent(
            DomainEventId: Guid.NewGuid(),
            Timestamp: dateTimeService.UtcNow,
            pageId);
    }
}