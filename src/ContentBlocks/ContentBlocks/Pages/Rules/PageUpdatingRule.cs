using FluentValidation;

namespace ChillSite.ContentBlocks.Pages.Rules;

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
            .NotEmpty().WithMessage("Заголовок страницы обязателен для заполнения.")
            .MaximumLength(titleMaximumLength).WithMessage(
                $"Длина заголовка страницы не должна превышать {titleMaximumLength} символов.");
        
        RuleFor(x => x.SeoDescription)
            .MaximumLength(seoFieldMaximumLength).WithMessage(
                $"Длина описания страницы для SEO не должна превышать {seoFieldMaximumLength} символов.");
        
        RuleFor(x => x.SeoKeywords)
            .MaximumLength(seoFieldMaximumLength).WithMessage(
                $"Длина перечня ключевых слов страницы для SEO не должна превышать {seoFieldMaximumLength} символов.");
    }
}