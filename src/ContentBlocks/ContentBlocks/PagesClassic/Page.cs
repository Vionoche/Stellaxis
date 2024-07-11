using System;
using ChillSite.ContentBlocks.Common;
using ChillSite.ContentBlocks.PagesClassic.Events;
using ChillSite.ContentBlocks.PagesClassic.Rules;
using FluentValidation;

namespace ChillSite.ContentBlocks.PagesClassic;

public class Page
{
    public Guid PageId { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public bool IsPublished { get; private set; }
    public DateTime? PublishedDate { get; private set; }
    public string? SeoDescription { get; private set; }
    public string? SeoKeywords { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? ModificationDate { get; private set; }

    public Page(
        IDateTimeService dateTimeService,
        IDomainEventHandler domainEventHandler,
        Guid pageId,
        string title,
        string? description,
        bool isPublished,
        DateTime? publishedDate,
        string? seoDescription,
        string? seoKeywords,
        DateTime creationDate,
        DateTime? modificationDate)
    {
        _dateTimeService = dateTimeService;
        _domainEventHandler = domainEventHandler;
        
        PageId = pageId;
        Title = title;
        Description = description;
        IsPublished = isPublished;
        PublishedDate = publishedDate;
        SeoDescription = seoDescription;
        SeoKeywords = seoKeywords;
        CreationDate = creationDate;
        ModificationDate = modificationDate;
    }

    public static Page Create(
        IDateTimeService dateTimeService,
        IDomainEventHandler domainEventHandler,
        string title,
        string? description,
        string? seoDescription,
        string? seoKeywords)
    {
        var rule = new PageUpdatingRule(title, description, seoDescription, seoKeywords);
        new PageUpdatingRuleValidator().ValidateAndThrow(rule);

        var page = new Page(
            dateTimeService,
            domainEventHandler,
            pageId: Guid.NewGuid(),
            title,
            description,
            isPublished: false,
            publishedDate: null,
            seoDescription,
            seoKeywords,
            creationDate: dateTimeService.UtcNow,
            modificationDate: null);
        
        domainEventHandler.AddEvent(new PageCreatedEvent(
            page.PageId,
            page.Title,
            page.Description,
            page.SeoDescription,
            page.SeoKeywords));

        return page;
    }

    public void Update(string title, string? description, string? seoDescription, string? seoKeywords)
    {
        var rule = new PageUpdatingRule(title, description, seoDescription, seoKeywords);
        new PageUpdatingRuleValidator().ValidateAndThrow(rule);

        Title = title;
        Description = description;
        SeoDescription = seoDescription;
        SeoKeywords = seoKeywords;
        ModificationDate = _dateTimeService.UtcNow;
        
        _domainEventHandler.AddEvent(new PageUpdatedEvent(
            PageId,
            Title,
            Description,
            SeoDescription,
            SeoKeywords));
    }

    public void Publish(DateTime? publishedDate = null)
    {
        var rule = new PagePublishingRule(IsPublished, publishedDate);
        new PagePublishingRuleValidator(_dateTimeService).ValidateAndThrow(rule);

        IsPublished = true;
        PublishedDate = publishedDate;
        ModificationDate = _dateTimeService.UtcNow;
        
        _domainEventHandler.AddEvent(new PagePublishedEvent(PageId, PublishedDate));
    }

    public void Unpublish()
    {
        var rule = new PageUnpublishingRule(IsPublished);
        new PageUnpublishingRuleValidator().ValidateAndThrow(rule);
        
        IsPublished = false;
        PublishedDate = null;
        ModificationDate = _dateTimeService.UtcNow;
        
        _domainEventHandler.AddEvent(new PageUnpublishedEvent(PageId));
    }

    private readonly IDateTimeService _dateTimeService;
    private readonly IDomainEventHandler _domainEventHandler;
}