using System;

namespace ChillSite.SiteBlocks.Pages.Events;

public record PageNameUpdatedEvent(
    Guid PageId,
    string Name);