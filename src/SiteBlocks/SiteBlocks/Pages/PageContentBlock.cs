namespace ChillSite.SiteBlocks.Pages;

public record PageContentBlock(
    PageContainerName Name,
    int ContainerPosition,
    ContentBlock ContentBlock);
