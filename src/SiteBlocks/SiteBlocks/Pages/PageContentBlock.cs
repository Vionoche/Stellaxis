using ChillSite.SiteBlocks.ContentBlocks;

namespace ChillSite.SiteBlocks.Pages;

public record PageContentBlock(
    PageContainer PageContainer,
    ContentBlock ContentBlock,
    int ContainerPosition);