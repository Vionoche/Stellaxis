using System;
using ChillSite.SiteBlocks.Common;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PageNameUpdatedEvent(
    DateTime Timestamp,
    Guid PageId)
    : DomainEventBase(Timestamp)
{
    public static PageNameUpdatedEvent Create(IDateTimeProvider dateTimeProvider, Guid pageId)
    {
        return new PageNameUpdatedEvent(
            Timestamp: dateTimeProvider.UtcNow,
            pageId);
    }
}