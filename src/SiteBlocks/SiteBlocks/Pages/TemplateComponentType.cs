using System;

namespace ChillSite.SiteBlocks.Pages;

public record TemplateComponentType(
    TemplateComponentTypeName Name,
    Type ComponentType,
    LayoutComponentType LayoutComponentType);
    
public record struct TemplateComponentTypeName(
    string Value);