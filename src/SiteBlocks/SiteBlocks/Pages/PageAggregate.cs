using System;
using ChillSite.SiteBlocks.Common;
using ChillSite.SiteBlocks.Pages.Events;
using ChillSite.SiteBlocks.Pages.Rules;
using FluentValidation;

namespace ChillSite.SiteBlocks.Pages;

public sealed class PageAggregate
{
    public Page Page { get; private set; }

    public PageAggregate(
        IDateTimeProvider dateTimeProvider,
        IDomainEventBuffer domainEventBuffer,
        Page page)
    {
        _dateTimeProvider = dateTimeProvider;
        _domainEventBuffer = domainEventBuffer;
        Page = page;
    }
    
    public void Update(string title, string? description, string? seoDescription, string? seoKeywords)
    {
        var rule = new PageUpdatingRule(title, description, seoDescription, seoKeywords);
        new PageUpdatingRuleValidator().ValidateAndThrow(rule);

        var updatedPage = Page with
        {
            Title = title,
            Description = description,
            SeoDescription = seoDescription,
            SeoKeywords = seoKeywords,
            ModificationDate = _dateTimeProvider.UtcNow
        };
        
        _domainEventBuffer.AddEvent(PageUpdatedEvent.Create(_dateTimeProvider, updatedPage.PageId));

        Page = updatedPage;
    }

    public void UpdateName(string name)
    {
        var rule = new PageNameUpdatingRule(name);
        new PageNameUpdatingRuleValidator().ValidateAndThrow(rule);

        var updatedPage = Page with
        {
            Name = name,
            ModificationDate = _dateTimeProvider.UtcNow
        };
        
        _domainEventBuffer.AddEvent(PageNameUpdatedEvent.Create(_dateTimeProvider, updatedPage.PageId));
        
        Page = updatedPage;
    }
    
    public void Publish(DateTime? publishedDate = null)
    {
        var rule = new PagePublishingRule(Page.IsPublished, publishedDate);
        new PagePublishingRuleValidator(_dateTimeProvider).ValidateAndThrow(rule);

        var publishedPage = Page with
        {
            IsPublished = true,
            PublicationDate = publishedDate,
            ModificationDate = _dateTimeProvider.UtcNow
        };
        
        _domainEventBuffer.AddEvent(PagePublishedEvent.Create(_dateTimeProvider, publishedPage.PageId));

        Page = publishedPage;
    }

    public void Unpublish()
    {
        var rule = new PageUnpublishingRule(Page.IsPublished);
        new PageUnpublishingRuleValidator().ValidateAndThrow(rule);

        var unpublishedPage = Page with
        {
            IsPublished = false,
            PublicationDate = null,
            ModificationDate = _dateTimeProvider.UtcNow
        };
        
        _domainEventBuffer.AddEvent(PageUnpublishedEvent.Create(_dateTimeProvider, Page.PageId));

        Page = unpublishedPage;
    }
    
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDomainEventBuffer _domainEventBuffer;
}