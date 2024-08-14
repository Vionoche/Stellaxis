using System;
using ChillSite.SiteBlocks.Common;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PagePublishedEvent(
    DateTime Timestamp,
    Guid PageId)
    : DomainEventBase(Timestamp)
{
    public static PagePublishedEvent Create(IDateTimeProvider dateTimeProvider, Guid pageId)
    {
        return new PagePublishedEvent(
            Timestamp: dateTimeProvider.UtcNow,
            pageId);
    }
}