using System;

namespace ChillSite.SiteBlocks.ContentBlocks;

public record ContentBlockComponentType(
    ContentBlockComponentTypeName Name,
    Type ComponentType);
    
public record ContentBlockComponentTypeName(
    string Value);