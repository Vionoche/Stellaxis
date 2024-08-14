using System;
using System.Collections.Generic;
using System.Linq;
using ChillSite.SiteBlocks.Common;
using ChillSite.SiteBlocks.ContentBlocks;
using ChillSite.SiteBlocks.Pages.Events;

namespace ChillSite.SiteBlocks.Pages;

public sealed class PageContentBlocksAggregate
{
    public Page Page { get; private set; }

    public IReadOnlyCollection<PageContentBlock> PageContentBlocks => _pageContentBlocks;
    
    public IReadOnlyDictionary<PageContainer, ContentBlock[]> ContentBlocksMap => _contentBlocksMap;

    public PageContentBlocksAggregate(
        IDateTimeProvider dateTimeProvider,
        IDomainEventBuffer domainEventBuffer,
        Page page,
        IEnumerable<PageContentBlock> pageContentBlocks)
    {
        _dateTimeProvider = dateTimeProvider;
        _domainEventBuffer = domainEventBuffer;
        Page = page;
        _pageContentBlocks = pageContentBlocks.ToList();
        _contentBlocksMap = CreateContentBlocksMap(_pageContentBlocks);
    }

    public void AddOrUpdatePageContentBlocks(params PageContentBlock[] pageContentBlocks)
    {
        if (pageContentBlocks.Length == 0)
        {
            return;
        }

        _pageContentBlocks.RemoveAll(current =>
            pageContentBlocks.Any(updated =>
                updated.PageContainer.Name.Equals(current.PageContainer.Name, StringComparison.InvariantCultureIgnoreCase)
                && updated.ContentBlock.ContentBlockId == current.ContentBlock.ContentBlockId));
        
        _pageContentBlocks.AddRange(pageContentBlocks);
        
        _contentBlocksMap = CreateContentBlocksMap(_pageContentBlocks);
        
        UpdatePageModificationDate();
        SendPageContentBlocksUpdatedEvent();
    }

    public void RemovePageContentBlocks(params PageContentBlock[] pageContentBlocks)
    {
        if (pageContentBlocks.Length == 0)
        {
            return;
        }
        
        foreach (var pageContentBlock in pageContentBlocks)
        {
            _pageContentBlocks.Remove(pageContentBlock);
        }
        
        _contentBlocksMap = CreateContentBlocksMap(_pageContentBlocks);
        
        UpdatePageModificationDate();
        SendPageContentBlocksUpdatedEvent();
    }

    private static Dictionary<PageContainer, ContentBlock[]> CreateContentBlocksMap(IEnumerable<PageContentBlock> pageContentBlocks)
    {
        return pageContentBlocks
            .GroupBy(x => x.PageContainer)
            .ToDictionary(
                grouping => grouping.Key,
                grouping => grouping
                    .OrderBy(pageContentBlock => pageContentBlock.ContainerPosition)
                    .Select(pageContentBlock => pageContentBlock.ContentBlock)
                    .ToArray());
    }

    private void UpdatePageModificationDate()
    {
        Page = Page with
        {
            ModificationDate = _dateTimeProvider.UtcNow
        };
    }

    private void SendPageContentBlocksUpdatedEvent()
    {
        _domainEventBuffer.AddEvent(PageContentBlocksUpdatedEvent.Create(_dateTimeProvider, Page.PageId));
    }

    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDomainEventBuffer _domainEventBuffer;
    private readonly List<PageContentBlock> _pageContentBlocks;
    private Dictionary<PageContainer, ContentBlock[]> _contentBlocksMap;
}