using System;
using ChillSite.SiteBlocks.Common;
using FluentValidation;

namespace ChillSite.SiteBlocks.Pages.Rules;

public record PagePublishingRule(
    bool IsPublished,
    DateTime? PublicationDate);

public class PagePublishingRuleValidator : AbstractValidator<PagePublishingRule>
{
    public PagePublishingRuleValidator(IDateTimeService dateTimeService)
    {
        RuleFor(x => x.IsPublished)
            .Equal(false)
            .WithMessage("The page has been already published.");
        
        RuleFor(x => x.PublicationDate)
            .GreaterThanOrEqualTo(dateTimeService.UtcNow)
            .When(x => x.PublicationDate != null)
            .WithMessage("The publication date must be greater than or equal to current date.");
    }
}