using AccountAPI.Models;
using FluentValidation;

namespace AccountAPI.Models.Validators
{
    public class ProductModelValidator : AbstractValidator<ProductModel>
    {
        public ProductModelValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} is mandatory}");
            RuleFor(p => p.Description).NotEmpty().WithMessage("{PropertyName} is mandatory}");
            RuleFor(p => p.Price).NotEmpty().WithMessage("{PropertyName} is mandatory}");
        }
    }
}
