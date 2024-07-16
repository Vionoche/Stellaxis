using System;
using ChillSite.ContentBlocks.Common;

namespace ChillSite.ContentBlocks.Pages;

public class PageManager : IPageManager
{
    public PageManager(IDateTimeService dateTimeService, IDomainEventHandler domainEventHandler)
    {
        _dateTimeService = dateTimeService;
        _domainEventHandler = domainEventHandler;
    }

    public Page Update(Page page, string title, string? description, string? seoDescription, string? seoKeywords)
    {
        return page.Update(_dateTimeService, _domainEventHandler, title, description, seoDescription, seoKeywords);
    }

    public Page Publish(Page page, DateTime? publishedDate = null)
    {
        return page.Publish(_dateTimeService, _domainEventHandler, publishedDate);
    }

    public Page Unpublish(Page page)
    {
        return page.Unpublish(_dateTimeService, _domainEventHandler);
    }

    private readonly IDateTimeService _dateTimeService;
    private readonly IDomainEventHandler _domainEventHandler;
}