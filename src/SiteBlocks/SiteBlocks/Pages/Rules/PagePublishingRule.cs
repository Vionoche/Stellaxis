using System;
using FluentValidation;
using Stellaxis.SiteBlocks.Common;

namespace Stellaxis.SiteBlocks.Pages.Rules;

public record PagePublishingRule(
    bool IsPublished,
    DateTime? PublicationDate);

public class PagePublishingRuleValidator : AbstractValidator<PagePublishingRule>
{
    public PagePublishingRuleValidator(IDateTimeProvider dateTimeProvider)
    {
        RuleFor(x => x.IsPublished)
            .Equal(false)
            .WithMessage("The page has been already published.");
        
        RuleFor(x => x.PublicationDate)
            .GreaterThanOrEqualTo(dateTimeProvider.UtcNow)
            .When(x => x.PublicationDate != null)
            .WithMessage("The publication date must be greater than or equal to current date.");
    }
}