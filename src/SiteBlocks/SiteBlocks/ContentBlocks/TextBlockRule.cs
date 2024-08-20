using FluentValidation;

namespace ChillSite.SiteBlocks.ContentBlocks;

public record TextBlockRule(
    string Text);

public class TextBlockRuleValidator : AbstractValidator<TextBlockRule>
{
    public TextBlockRuleValidator()
    {
        
    }
}