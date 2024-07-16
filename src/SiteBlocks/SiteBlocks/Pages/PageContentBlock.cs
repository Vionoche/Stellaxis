using System;
using ChillSite.SiteBlocks.ContentBlocks;

namespace ChillSite.SiteBlocks.Pages;

public record PageContentBlock(
    Guid PageId,
    ContentBlock ContentBlock,
    PageContainer PageContainer,
    int ContainerPosition);