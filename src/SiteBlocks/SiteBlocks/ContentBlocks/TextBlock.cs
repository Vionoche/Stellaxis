using System;
using ChillSite.SiteBlocks.ContentBlocks.Options;

namespace ChillSite.SiteBlocks.ContentBlocks;

public sealed record TextBlock(
    Guid ContentBlockId,
    ContentBlockComponentType ContentBlockComponentType,
    bool IsShared,
    TextOptions Options,
    DateTime CreationDate,
    DateTime? ModificationDate)
    : ContentBlock<TextOptions>(
        ContentBlockId,
        ContentBlockComponentType,
        Options,
        IsShared,
        CreationDate,
        ModificationDate);