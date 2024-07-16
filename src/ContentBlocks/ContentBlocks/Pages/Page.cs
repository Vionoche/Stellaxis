using System;
using ChillSite.ContentBlocks.Common;
using ChillSite.ContentBlocks.Pages.Events;
using ChillSite.ContentBlocks.Pages.Rules;
using FluentValidation;

namespace ChillSite.ContentBlocks.Pages;

public record Page(
    Guid PageId,
    string Title,
    string? Description,
    bool IsPublished,
    DateTime? PublishedDate,
    string? SeoDescription,
    string? SeoKeywords,
    DateTime CreationDate,
    DateTime? ModificationDate)
{
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
            PageId: Guid.NewGuid(),
            title,
            description,
            IsPublished: false,
            PublishedDate: null,
            seoDescription,
            seoKeywords,
            CreationDate: dateTimeService.UtcNow,
            ModificationDate: null);
        
        domainEventHandler.AddEvent(new PageCreatedEvent(
            page.PageId,
            page.Title,
            page.Description,
            page.SeoDescription,
            page.SeoKeywords));

        return page;
    }
}