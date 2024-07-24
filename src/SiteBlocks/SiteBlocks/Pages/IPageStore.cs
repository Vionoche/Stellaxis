using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChillSite.SiteBlocks.Pages;

public interface IPageStore
{
    Task<Page> GetPage(Guid pageId, CancellationToken cancellationToken);

    Task SavePage(Page page, CancellationToken cancellationToken);
}