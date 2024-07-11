using System;

namespace ChillSite.ContentBlocks.PagesClassic.Events;

public record PageCreatedEvent(
    Guid PageId,
    string Title,
    string? Description,
    string? SeoDescription,
    string? SeoKeywords);