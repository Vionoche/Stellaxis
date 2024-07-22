using System.Collections.Generic;
using System.Linq;
using ChillSite.SiteBlocks.Common;
using ChillSite.SiteBlocks.ContentBlocks;
using ChillSite.SiteBlocks.Pages.Events;

namespace ChillSite.SiteBlocks.Pages;

public class PageContentBlocksAggregate
{
    public Page Page { get; private set; }

    public IReadOnlyCollection<PageContentBlock> PageContentBlocks => _pageContentBlocks;
    
    public IReadOnlyDictionary<PageContainer, ContentBlock[]> ContentBlocksMap => _contentBlocksMap;

    public PageContentBlocksAggregate(
        IDateTimeService dateTimeService,
        IDomainEventHandler domainEventHandler,
        Page page,
        IEnumerable<PageContentBlock> pageContentBlocks)
    {
        _dateTimeService = dateTimeService;
        _domainEventHandler = domainEventHandler;
        Page = page;
        _pageContentBlocks = pageContentBlocks.ToList();
        _contentBlocksMap = CreateContentBlocksMap(_pageContentBlocks);
    }

    public void AddPageContentBlocks(params PageContentBlock[] pageContentBlocks)
    {
        if (pageContentBlocks.Length == 0)
        {
            return;
        }
        
        _pageContentBlocks.AddRange(pageContentBlocks);
        _contentBlocksMap = CreateContentBlocksMap(_pageContentBlocks);

        UpdatePageModificationDate();
        SendPageContentBlocksUpdatedEvent();
    }

    public void UpdatePageContentBlocks(params PageContentBlock[] pageContentBlocks)
    {
        if (pageContentBlocks.Length == 0)
        {
            return;
        }
        
        // todo: merge with AddPageContentBlocks
        
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
            ModificationDate = _dateTimeService.UtcNow
        };
    }

    private void SendPageContentBlocksUpdatedEvent()
    {
        _domainEventHandler.AddEvent(new PageContentBlocksUpdated(Page.PageId));
    }

    private readonly IDateTimeService _dateTimeService;
    private readonly IDomainEventHandler _domainEventHandler;
    private readonly List<PageContentBlock> _pageContentBlocks;
    private Dictionary<PageContainer, ContentBlock[]> _contentBlocksMap;
}