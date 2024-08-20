using System;

namespace ChillSite.SiteBlocks.Pages;

public abstract record ContentBlock(
    Guid ContentBlockId,
    ContentBlockComponentType ContentBlockComponentType);