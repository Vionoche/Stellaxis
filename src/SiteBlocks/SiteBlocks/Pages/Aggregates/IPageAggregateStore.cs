using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChillSite.SiteBlocks.Pages.Aggregates;

public interface IPageAggregateStore
{
    Task<PageAggregate> GetPageAggregate(Guid pageId, CancellationToken cancellationToken);

    Task SavePageAggregate(PageAggregate pageAggregate, CancellationToken cancellationToken);
    
    Task<PageContentBlocksAggregate> GetPageContentBlocksAggregate(Guid pageId, CancellationToken cancellationToken);

    Task SavePageContentBlocksAggregate(PageContentBlocksAggregate pageContentBlocksAggregate, CancellationToken cancellationToken);
}