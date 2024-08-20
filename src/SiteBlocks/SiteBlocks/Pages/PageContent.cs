using System.Collections.Generic;

namespace ChillSite.SiteBlocks.Pages;

public record PageContent(
    Page Page,
    IDictionary<PageContainerName, PageContainer> PageContainers);