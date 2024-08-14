using System;
using ChillSite.SiteBlocks.Common;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PageCreatedEvent(
    DateTime Timestamp,
    Guid PageId)
    : DomainEventBase(Timestamp)
{
    public static PageCreatedEvent Create(IDateTimeProvider dateTimeProvider, Guid pageId)
    {
        return new PageCreatedEvent(
            Timestamp: dateTimeProvider.UtcNow,
            pageId);
    }
}