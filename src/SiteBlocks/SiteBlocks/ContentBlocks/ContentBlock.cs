using System;

namespace ChillSite.SiteBlocks.ContentBlocks;

public record ContentBlock<TOptions>(
    Guid ContentBlockId,
    ContentBlockComponentType ContentBlockComponentType,
    TOptions Options,
    bool IsShared,
    DateTime CreationDate,
    DateTime? ModificationDate)
    : ContentBlock(ContentBlockId, ContentBlockComponentType, IsShared, CreationDate, ModificationDate);

public abstract record ContentBlock(
    Guid ContentBlockId,
    ContentBlockComponentType ContentBlockComponentType,
    bool IsShared,
    DateTime CreationDate,
    DateTime? ModificationDate);