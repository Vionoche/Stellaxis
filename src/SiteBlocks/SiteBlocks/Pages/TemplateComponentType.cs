using System;

namespace ChillSite.SiteBlocks.Pages;

public record TemplateComponentType(
    TemplateComponentName ComponentName,
    Type Type,
    LayoutComponentType LayoutComponentType);