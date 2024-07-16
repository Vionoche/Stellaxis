using System;
using ChillSite.SiteBlocks.ContentBlocks;

namespace ChillSite.SiteBlocks.Pages;

public record PageContentBlock(
    Guid SitePageId,
    ContentBlock ContentBlock,
    PageContainer PageContainer,
    int ContainerPosition);