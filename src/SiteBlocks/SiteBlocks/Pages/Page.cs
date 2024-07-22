using System;
using ChillSite.SiteBlocks.Common;
using ChillSite.SiteBlocks.Pages.Events;
using ChillSite.SiteBlocks.Pages.Rules;
using FluentValidation;

namespace ChillSite.SiteBlocks.Pages;

public record Page(
    Guid PageId,
    TemplateComponentType TemplateComponentType,
    string Title,
    string? Description,
    bool IsPublished,
    DateTime? PublicationDate,
    string? SeoDescription,
    string? SeoKeywords,
    DateTime CreationDate,
    DateTime? ModificationDate)
{
    public static Page Create(
        IDateTimeService dateTimeService,
        IDomainEventHandler domainEventHandler,
        TemplateComponentType templateComponentType,
        string title,
        string? description,
        string? seoDescription,
        string? seoKeywords)
    {
        var rule = new PageUpdatingRule(title, description, seoDescription, seoKeywords);
        new PageUpdatingRuleValidator().ValidateAndThrow(rule);

        var page = new Page(
            PageId: Guid.NewGuid(),
            templateComponentType,
            title,
            description,
            IsPublished: false,
            PublicationDate: null,
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