using System;
using Stellaxis.SiteBlocks.Common;

namespace Stellaxis.SiteBlocks.Pages.Events;

public record PageUpdatedEvent : DomainEvent
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