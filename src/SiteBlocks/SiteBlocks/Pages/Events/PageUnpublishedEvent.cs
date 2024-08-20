using System;
using ChillSite.SiteBlocks.Common;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PageUnpublishedEvent : DomainEvent
{
    public Guid PageId { get; }

    public PageUnpublishedEvent(
        Guid eventId,
        DateTime occuredOn,
        Guid pageId) : base(eventId, occuredOn)
    {
        PageId = pageId;
    }
    
    public static PageUnpublishedEvent Create(IDateTimeProvider dateTimeProvider, Guid pageId)
    {
        return new PageUnpublishedEvent(
            eventId: Guid.NewGuid(),
            occuredOn: dateTimeProvider.UtcNow,
            pageId);
    }
}