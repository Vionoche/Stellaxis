using System;
using ChillSite.SiteBlocks.Common;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PageContentBlocksUpdatedEvent(
    DateTime Timestamp,
    Guid PageId)
    : DomainEventBase(Timestamp)
{
    public static PageContentBlocksUpdatedEvent Create(IDateTimeProvider dateTimeProvider, Guid pageId)
    {
        return new PageContentBlocksUpdatedEvent(
            Timestamp: dateTimeProvider.UtcNow,
            pageId);
    }
}