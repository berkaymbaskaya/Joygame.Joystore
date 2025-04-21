using FluentValidation;
using Joygame.Joystore.API.Models.Product;

namespace Joygame.Joystore.API.Validators
{
    public class ProductCreateValidator : AbstractValidator<ProductCreateRequestDto>
    {
        public ProductCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.CatId)
                .GreaterThan(0).WithMessage("Category must be selected.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description can't be longer than 500 characters.");
        }
    }
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateRequestDto>
    {
        public ProductUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.CatId)
                .GreaterThan(0).WithMessage("Category must be selected.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description can't be longer than 500 characters.");
        }
    }
}
