using System;
using ChillSite.SiteBlocks.Common;
using ChillSite.SiteBlocks.Pages.Events;
using ChillSite.SiteBlocks.Pages.Rules;
using FluentValidation;

namespace ChillSite.SiteBlocks.Pages;

public record Page(
    Guid PageId,
    TemplateComponentType TemplateComponentType,
    string Name,
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
        string name,
        string title,
        string? description,
        string? seoDescription,
        string? seoKeywords)
    {
        var nameUpdatingRule = new PageNameUpdatingRule(name);
        new PageNameUpdatingRuleValidator().ValidateAndThrow(nameUpdatingRule);
        
        var updatingRule = new PageUpdatingRule(title, description, seoDescription, seoKeywords);
        new PageUpdatingRuleValidator().ValidateAndThrow(updatingRule);

        var page = new Page(
            PageId: Guid.NewGuid(),
            templateComponentType,
            name,
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
            page.Name,
            page.Title,
            page.Description,
            page.SeoDescription,
            page.SeoKeywords));

        return page;
    }
}