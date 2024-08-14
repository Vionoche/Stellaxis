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
        IDateTimeProvider dateTimeProvider,
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
            ModificationDate = dateTimeProvider.UtcNow
        };
        
        domainEventBuffer.AddEvent(PageUpdatedEvent.Create(dateTimeProvider, updatedPage.PageId));

        return updatedPage;
    }

    public static Page UpdateName(
        this Page page,
        IDateTimeProvider dateTimeProvider,
        IDomainEventBuffer domainEventBuffer,
        string name)
    {
        var rule = new PageNameUpdatingRule(name);
        new PageNameUpdatingRuleValidator().ValidateAndThrow(rule);

        var updatedPage = page with
        {
            Name = name,
            ModificationDate = dateTimeProvider.UtcNow
        };
        
        domainEventBuffer.AddEvent(PageNameUpdatedEvent.Create(dateTimeProvider, updatedPage.PageId));
        
        return updatedPage;
    }
    
    public static Page Publish(
        this Page page,
        IDateTimeProvider dateTimeProvider,
        IDomainEventBuffer domainEventBuffer,
        DateTime? publishedDate = null)
    {
        var rule = new PagePublishingRule(page.IsPublished, publishedDate);
        new PagePublishingRuleValidator(dateTimeProvider).ValidateAndThrow(rule);

        var publishedPage = page with
        {
            IsPublished = true,
            PublicationDate = publishedDate,
            ModificationDate = dateTimeProvider.UtcNow
        };
        
        domainEventBuffer.AddEvent(PagePublishedEvent.Create(dateTimeProvider, publishedPage.PageId));

        return publishedPage;
    }

    public static Page Unpublish(
        this Page page,
        IDateTimeProvider dateTimeProvider,
        IDomainEventBuffer domainEventBuffer)
    {
        var rule = new PageUnpublishingRule(page.IsPublished);
        new PageUnpublishingRuleValidator().ValidateAndThrow(rule);

        var unpublishedPage = page with
        {
            IsPublished = false,
            PublicationDate = null,
            ModificationDate = dateTimeProvider.UtcNow
        };
        
        domainEventBuffer.AddEvent(PageUnpublishedEvent.Create(dateTimeProvider, page.PageId));

        return unpublishedPage;
    }
}