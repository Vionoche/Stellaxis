using System.Collections.Generic;

namespace Stellaxis.SiteBlocks.Pages;

public record PageContent(
    Page Page,
    IReadOnlyDictionary<PageContainerName, PageContainer> PageContainers);