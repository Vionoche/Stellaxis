using System;

namespace ChillSite.SiteBlocks.ContentBlocks;

public record ContentBlockComponentType(
    ContentBlockComponentName Name,
    Type Type);