using System.Collections.Generic;
using Stellaxis.SiteBlocks.Common;
using Stellaxis.SiteBlocks.Pages.Events;

namespace Stellaxis.SiteBlocks.Pages.Aggregates;

public sealed class PageContentAggregate
{
    public Page Page { get; private set; }

    public IReadOnlyDictionary<PageContainerName, PageContainer> PageContainers => _pageContainers;

    public PageContentAggregate(
        IDateTimeProvider dateTimeProvider,
        IDomainEventBuffer domainEventBuffer,
        Page page,
        Dictionary<PageContainerName, PageContainer> pageContainers)
    {
        _dateTimeProvider = dateTimeProvider;
        _domainEventBuffer = domainEventBuffer;
        Page = page;
        _pageContainers = pageContainers;
    }

    // public void AddOrUpdatePageContentBlocks(params PageContentBlock[] pageContentBlocks)
    // {
    //     if (pageContentBlocks.Length == 0)
    //     {
    //         return;
    //     }
    //
    //     _pageContentBlocks.RemoveAll(current =>
    //         pageContentBlocks.Any(updated =>
    //             updated.PageContainer.Name.Equals(current.PageContainer.Name, StringComparison.InvariantCultureIgnoreCase)
    //             && updated.ContentBlock.ContentBlockId == current.ContentBlock.ContentBlockId));
    //     
    //     _pageContentBlocks.AddRange(pageContentBlocks);
    //     
    //     _contentBlocksMap = CreateContentBlocksMap(_pageContentBlocks);
    //     
    //     UpdatePageModificationDate();
    //     SendPageContentBlocksUpdatedEvent();
    // }
    //
    // public void RemovePageContentBlocks(params PageContentBlock[] pageContentBlocks)
    // {
    //     if (pageContentBlocks.Length == 0)
    //     {
    //         return;
    //     }
    //     
    //     foreach (var pageContentBlock in pageContentBlocks)
    //     {
    //         _pageContentBlocks.Remove(pageContentBlock);
    //     }
    //     
    //     _contentBlocksMap = CreateContentBlocksMap(_pageContentBlocks);
    //     
    //     UpdatePageModificationDate();
    //     SendPageContentBlocksUpdatedEvent();
    // }

    // private static Dictionary<PageContainer, ContentBlock[]> CreateContentBlocksMap(IEnumerable<PageContentBlock> pageContentBlocks)
    // {
    //     return pageContentBlocks
    //         .GroupBy(x => x.PageContainer)
    //         .ToDictionary(
    //             grouping => grouping.Key,
    //             grouping => grouping
    //                 .OrderBy(pageContentBlock => pageContentBlock.ContainerPosition)
    //                 .Select(pageContentBlock => pageContentBlock.ContentBlock)
    //                 .ToArray());
    // }

    private void UpdatePageModificationDate()
    {
        Page = Page with
        {
            ModificationDate = _dateTimeProvider.UtcNow
        };
    }

    private void SendPageContentBlocksUpdatedEvent()
    {
        _domainEventBuffer.AddEvent(PageContentUpdatedEvent.Create(_dateTimeProvider, Page.PageId));
    }

    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDomainEventBuffer _domainEventBuffer;
    private readonly Dictionary<PageContainerName, PageContainer> _pageContainers;
}