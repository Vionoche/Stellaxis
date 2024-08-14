using System;
using System.Threading;
using System.Threading.Tasks;
using ChillSite.SiteBlocks.Pages;

namespace ChillSite.SiteBlocks.Storage.FileStorage.Stores;

public class PageStore : IPageStore
{
    public Task<Page> GetPage(Guid pageId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}