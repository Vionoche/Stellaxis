using System;
using ChillSite.SiteBlocks.Common;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PageNameUpdatedEvent : DomainEvent
{
    public Guid PageId { get; }

    public PageNameUpdatedEvent(
        Guid eventId,
        DateTime occuredOn,
        Guid pageId) : base(eventId, occuredOn)
    {
        PageId = pageId;
    }
    
    public static PageNameUpdatedEvent Create(IDateTimeProvider dateTimeProvider, Guid pageId)
    {
        return new PageNameUpdatedEvent(
            eventId: Guid.NewGuid(),
            occuredOn: dateTimeProvider.UtcNow,
            pageId);
    }
}