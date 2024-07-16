using System;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PagePublishedEvent(
    Guid PageId,
    DateTime? PublishedDate);