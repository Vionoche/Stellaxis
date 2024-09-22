using System.Text.RegularExpressions;
using FluentValidation;

namespace Stellaxis.SiteBlocks.Pages.Rules;

public record PageNameUpdatingRule(
    string Name);

public class PageNameUpdatingRuleValidator : AbstractValidator<PageNameUpdatingRule>
{
    public PageNameUpdatingRuleValidator()
    {
        const int nameMaximumLength = 20;

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The name must not be empty.")
            .MaximumLength(nameMaximumLength)
            .WithMessage($"The name's length must be less than or equal to {nameMaximumLength}.")
            .Matches(NameRegexRule)
            .WithMessage("The name contains invalid symbols.");
    }

    private static readonly Regex NameRegexRule = new(@"^[\w-]*$");
}