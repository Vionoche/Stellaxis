using System;

namespace ChillSite.ContentBlocks.PagesClassic.Events;

public record PagePublishedEvent(
    Guid PageId,
    DateTime? PublishedDate);