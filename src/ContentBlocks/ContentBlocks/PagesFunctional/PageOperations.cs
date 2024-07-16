using System;
using ChillSite.ContentBlocks.Common;
using ChillSite.ContentBlocks.PagesClassic.Events;
using ChillSite.ContentBlocks.PagesClassic.Rules;
using FluentValidation;

namespace ChillSite.ContentBlocks.PagesFunctional;

public static class PageOperations
{
    public static Page Update(
        this Page page, IDateTimeService dateTimeService, IDomainEventHandler domainEventHandler,
        string title, string? description, string? seoDescription, string? seoKeywords)
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
        
        domainEventHandler.AddEvent(new PageUpdatedEvent(
            updatedPage.PageId,
            updatedPage.Title,
            updatedPage.Description,
            updatedPage.SeoDescription,
            updatedPage.SeoKeywords));

        return updatedPage;
    }
    
    public static Page Publish(
        this Page page, IDateTimeService dateTimeService, IDomainEventHandler domainEventHandler, DateTime? publishedDate = null)
    {
        var rule = new PagePublishingRule(page.IsPublished, publishedDate);
        new PagePublishingRuleValidator(dateTimeService).ValidateAndThrow(rule);

        var publishedPage = page with
        {
            IsPublished = true,
            PublishedDate = publishedDate,
            ModificationDate = dateTimeService.UtcNow
        };
        
        domainEventHandler.AddEvent(new PagePublishedEvent(
            publishedPage.PageId,
            publishedPage.PublishedDate));

        return publishedPage;
    }

    public static Page Unpublish(
        this Page page, IDateTimeService dateTimeService, IDomainEventHandler domainEventHandler)
    {
        var rule = new PageUnpublishingRule(page.IsPublished);
        new PageUnpublishingRuleValidator().ValidateAndThrow(rule);

        var unpublishedPage = page with
        {
            IsPublished = false,
            PublishedDate = null,
            ModificationDate = dateTimeService.UtcNow
        };
        
        domainEventHandler.AddEvent(new PageUnpublishedEvent(page.PageId));

        return unpublishedPage;
    }
}