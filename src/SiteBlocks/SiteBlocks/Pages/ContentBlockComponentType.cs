using System;

namespace Stellaxis.SiteBlocks.Pages;

public record ContentBlockComponentType(
    ContentBlockComponentTypeName Name,
    Type ComponentType);
    
public record struct ContentBlockComponentTypeName(
    string Value);