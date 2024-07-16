using System;

namespace ChillSite.SiteBlocks.Pages;

public record LayoutComponentType(
    LayoutComponentName ComponentName,
    Type Type);