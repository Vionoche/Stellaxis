using System;

namespace Stellaxis.SiteBlocks.Pages;

public record LayoutComponentType(
    LayoutComponentTypeName Name,
    Type ComponentType);
    
public record struct LayoutComponentTypeName(
    string Value);