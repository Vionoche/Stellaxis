using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChillSite.SiteBlocks.Pages;

public interface IPageStore
{
    Task<Page> GetPage(Guid pageId, CancellationToken cancellationToken);

    // todo: path = "home", "home/about", "home/about/company", "home/contacts"
    //Task<Page> GetPageByPath(string path, CancellationToken cancellationToken);
}