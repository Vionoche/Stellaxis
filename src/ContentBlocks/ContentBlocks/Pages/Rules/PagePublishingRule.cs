using System;
using ChillSite.ContentBlocks.Common;
using FluentValidation;

namespace ChillSite.ContentBlocks.Pages.Rules;

public record PagePublishingRule(
    bool IsPublished,
    DateTime? PublishedDate);

public class PagePublishingRuleValidator : AbstractValidator<PagePublishingRule>
{
    public PagePublishingRuleValidator(IDateTimeService dateTimeService)
    {
        RuleFor(x => x.IsPublished)
            .Equal(false)
            .WithMessage($"The page already has been published.");
        
        RuleFor(x => x.PublishedDate)
            .GreaterThanOrEqualTo(dateTimeService.UtcNow)
            .WithMessage("Дата публикации страницы не может быть меньше текущей даты.");
    }
}