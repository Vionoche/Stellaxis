using System;
using Stellaxis.SiteBlocks.Common;

namespace Stellaxis.SiteBlocks.Pages.Events;

public record PageCreatedEvent : DomainEvent
{
    public Guid PageId { get; }
    
    public PageCreatedEvent(
        Guid eventId,
        DateTime occuredOn,
        Guid pageId) : base(eventId, occuredOn)
    {
        PageId = pageId;
    }
    
    public static PageCreatedEvent Create(IDateTimeProvider dateTimeProvider, Guid pageId)
    {
        return new PageCreatedEvent(
            eventId: Guid.NewGuid(),
            occuredOn: dateTimeProvider.UtcNow,
            pageId);
    }
}