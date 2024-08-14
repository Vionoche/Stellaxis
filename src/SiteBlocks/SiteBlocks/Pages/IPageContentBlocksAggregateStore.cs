using System;
using System.Threading;
using System.Threading.Tasks;
using ChillSite.SiteBlocks.Pages.Aggregates;

namespace ChillSite.SiteBlocks.Pages;

public interface IPageContentBlocksAggregateStore
{
    Task<PageContentBlocksAggregate> GetPageContentBlocksAggregate(Guid pageId, CancellationToken cancellationToken);

    Task SavePageContentBlocksAggregate(PageContentBlocksAggregate pageContentBlocksAggregate, CancellationToken cancellationToken);
}