using FluentValidation;
using NetChallengeTest.Core.Models;

namespace NetChallengeTest.Core.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.ProductCode).NotEmpty();
        RuleFor(x => x.ProductName).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}
