using System;

namespace ChillSite.ContentBlocks.Pages.Events;

public record PageUpdatedEvent(
    Guid PageId,
    string Title,
    string? Description,
    string? SeoDescription,
    string? SeoKeywords);