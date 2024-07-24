using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChillSite.SiteBlocks.Pages;

public interface IPageContentBlocksAggregateStore
{
    Task<PageContentBlocksAggregate> GetPageContentBlocksAggregate(Guid pageId, CancellationToken cancellationToken);

    Task SavePageContentBlocksAggregate(PageContentBlocksAggregate pageContentBlocksAggregate, CancellationToken cancellationToken);
}