using System;
using ChillSite.SiteBlocks.Common;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PageUnpublishedEvent(
    DateTime Timestamp,
    Guid PageId)
    : DomainEventBase(Timestamp)
{
    public static PageUnpublishedEvent Create(IDateTimeProvider dateTimeProvider, Guid pageId)
    {
        return new PageUnpublishedEvent(
            Timestamp: dateTimeProvider.UtcNow,
            pageId);
    }
}