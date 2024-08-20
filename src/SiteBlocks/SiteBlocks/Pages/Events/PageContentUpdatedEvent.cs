using System;
using ChillSite.SiteBlocks.Common;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PageContentUpdatedEvent : DomainEventBase
{
    public Guid PageId { get; }

    public PageContentUpdatedEvent(
        Guid eventId,
        DateTime occuredOn,
        Guid pageId) : base(eventId, occuredOn)
    {
        PageId = pageId;
    }
    
    public static PageContentUpdatedEvent Create(IDateTimeProvider dateTimeProvider, Guid pageId)
    {
        return new PageContentUpdatedEvent(
            eventId: Guid.NewGuid(),
            occuredOn: dateTimeProvider.UtcNow,
            pageId);
    }
}