using System.Collections.Generic;

namespace ChillSite.SiteBlocks.Pages;

public record PageContent(
    Page Page,
    IReadOnlyDictionary<PageContainerName, PageContainer> PageContainers);