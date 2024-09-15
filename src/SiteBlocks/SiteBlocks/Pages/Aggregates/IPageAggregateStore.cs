using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stellaxis.SiteBlocks.Pages.Aggregates;

public interface IPageAggregateStore
{
    Task<PageAggregate> GetPageAggregate(Guid pageId, CancellationToken cancellationToken);

    Task SavePageAggregate(PageAggregate pageAggregate, CancellationToken cancellationToken);
    
    Task<PageContentAggregate> GetPageContentBlocksAggregate(Guid pageId, CancellationToken cancellationToken);

    Task SavePageContentBlocksAggregate(PageContentAggregate pageContentAggregate, CancellationToken cancellationToken);
}