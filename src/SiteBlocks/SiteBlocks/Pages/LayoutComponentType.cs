using System;

namespace ChillSite.SiteBlocks.Pages;

public record LayoutComponentType(
    LayoutComponentTypeName Name,
    Type ComponentType);
    
public record struct LayoutComponentTypeName(
    string Value);