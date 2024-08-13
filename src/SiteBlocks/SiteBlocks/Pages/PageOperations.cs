using System;
using ChillSite.SiteBlocks.Common;
using ChillSite.SiteBlocks.Pages.Events;
using ChillSite.SiteBlocks.Pages.Rules;
using FluentValidation;

namespace ChillSite.SiteBlocks.Pages;

public static class PageOperations
{
    public static Page Update(
        this Page page,
        IDateTimeService dateTimeService,
        IDomainEventBuffer domainEventBuffer,
        string title,
        string? description,
        string? seoDescription,
        string? seoKeywords)
    {
        var rule = new PageUpdatingRule(title, description, seoDescription, seoKeywords);
        new PageUpdatingRuleValidator().ValidateAndThrow(rule);

        var updatedPage = page with
        {
            Title = title,
            Description = description,
            SeoDescription = seoDescription,
            SeoKeywords = seoKeywords,
            ModificationDate = dateTimeService.UtcNow
        };
        
        domainEventBuffer.AddEvent(PageUpdatedDomainEvent.Create(dateTimeService, updatedPage.PageId));

        return updatedPage;
    }

    public static Page UpdateName(
        this Page page,
        IDateTimeService dateTimeService,
        IDomainEventBuffer domainEventBuffer,
        string name)
    {
        var rule = new PageNameUpdatingRule(name);
        new PageNameUpdatingRuleValidator().ValidateAndThrow(rule);

        var updatedPage = page with
        {
            Name = name,
            ModificationDate = dateTimeService.UtcNow
        };
        
        domainEventBuffer.AddEvent(PageNameUpdatedDomainEvent.Create(dateTimeService, updatedPage.PageId));
        
        return updatedPage;
    }
    
    public static Page Publish(
        this Page page,
        IDateTimeService dateTimeService,
        IDomainEventBuffer domainEventBuffer,
        DateTime? publishedDate = null)
    {
        var rule = new PagePublishingRule(page.IsPublished, publishedDate);
        new PagePublishingRuleValidator(dateTimeService).ValidateAndThrow(rule);

        var publishedPage = page with
        {
            IsPublished = true,
            PublicationDate = publishedDate,
            ModificationDate = dateTimeService.UtcNow
        };
        
        domainEventBuffer.AddEvent(PagePublishedDomainEvent.Create(dateTimeService, publishedPage.PageId));

        return publishedPage;
    }

    public static Page Unpublish(
        this Page page,
        IDateTimeService dateTimeService,
        IDomainEventBuffer domainEventBuffer)
    {
        var rule = new PageUnpublishingRule(page.IsPublished);
        new PageUnpublishingRuleValidator().ValidateAndThrow(rule);

        var unpublishedPage = page with
        {
            IsPublished = false,
            PublicationDate = null,
            ModificationDate = dateTimeService.UtcNow
        };
        
        domainEventBuffer.AddEvent(PageUnpublishedDomainEvent.Create(dateTimeService, page.PageId));

        return unpublishedPage;
    }
}