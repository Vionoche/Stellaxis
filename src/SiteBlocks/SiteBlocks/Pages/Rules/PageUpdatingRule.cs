using FluentValidation;

namespace Stellaxis.SiteBlocks.Pages.Rules;

public record PageUpdatingRule(
    string Title,
    string? Description,
    string? SeoDescription,
    string? SeoKeywords);

public class PageUpdatingRuleValidator : AbstractValidator<PageUpdatingRule>
{
    public PageUpdatingRuleValidator()
    {
        const int titleMaximumLength = 500;
        const int seoFieldMaximumLength = 2000;

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("The title must not be empty.")
            .MaximumLength(titleMaximumLength)
            .WithMessage($"The title's length must be less than or equal to {titleMaximumLength}.");

        RuleFor(x => x.SeoDescription)
            .MaximumLength(seoFieldMaximumLength)
            .WithMessage($"The SEO description's length must be less than or equal to {seoFieldMaximumLength}.");

        RuleFor(x => x.SeoKeywords)
            .MaximumLength(seoFieldMaximumLength)
            .WithMessage($"The SEO keywords field's length must be less than or equal to {seoFieldMaximumLength}.");
    }
}