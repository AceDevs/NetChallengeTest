using FluentValidation;
using NetChallengeTest.Core.Models;

namespace NetChallengeTest.Core.Validators;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(x => x.CategoryCode).NotEmpty();
        RuleFor(x => x.CategoryName).NotEmpty();
    }
}
