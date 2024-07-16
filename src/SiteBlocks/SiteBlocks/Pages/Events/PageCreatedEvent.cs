using System;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PageCreatedEvent(
    Guid PageId,
    string Title,
    string? Description,
    string? SeoDescription,
    string? SeoKeywords);