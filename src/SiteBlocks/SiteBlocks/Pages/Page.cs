using System;
using FluentValidation;
using Stellaxis.SiteBlocks.Common;
using Stellaxis.SiteBlocks.Pages.Events;
using Stellaxis.SiteBlocks.Pages.Rules;

namespace Stellaxis.SiteBlocks.Pages;

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
        IDateTimeProvider dateTimeProvider,
        IDomainEventBuffer domainEventBuffer,
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
            CreationDate: dateTimeProvider.UtcNow,
            ModificationDate: null);
        
        domainEventBuffer.AddEvent(PageCreatedEvent.Create(dateTimeProvider, page.PageId));

        return page;
    }
}