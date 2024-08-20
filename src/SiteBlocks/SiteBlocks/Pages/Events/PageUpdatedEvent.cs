using System;
using ChillSite.SiteBlocks.Common;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PageUpdatedEvent : DomainEventBase
{
    public Guid PageId { get; }

    public PageUpdatedEvent(
        Guid eventId,
        DateTime occuredOn,
        Guid pageId) : base(eventId, occuredOn)
    {
        PageId = pageId;
    }
    
    public static PageUpdatedEvent Create(IDateTimeProvider dateTimeProvider, Guid pageId)
    {
        return new PageUpdatedEvent(
            eventId: Guid.NewGuid(),
            occuredOn: dateTimeProvider.UtcNow,
            pageId);
    }
}