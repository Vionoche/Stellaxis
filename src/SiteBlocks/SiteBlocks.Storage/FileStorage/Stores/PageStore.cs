using System;
using System.Threading;
using System.Threading.Tasks;
using Stellaxis.SiteBlocks.Pages;

namespace Stellaxis.SiteBlocks.Storage.FileStorage.Stores;

public class PageStore : IPageStore
{
    public Task<Page> GetPage(Guid pageId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}