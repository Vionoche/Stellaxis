using System;

namespace ChillSite.ContentBlocks.Pages.Events;

public record PagePublishedEvent(
    Guid PageId,
    DateTime? PublishedDate);