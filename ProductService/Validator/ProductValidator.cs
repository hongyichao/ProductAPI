using FluentValidation;
using ProductBusiness.Dtos;

namespace ProductBusiness.Validator
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required field")
                .Length(1, 200).WithMessage("The valid length of Name field is 1 to 200");
            RuleFor(x => x.Group)
                .NotNull().WithMessage("Group is required field")
                .Length(1, 200).WithMessage("The valid length of Name field is 1 to 200");
            RuleFor(x => x.Price)
                .NotNull().WithMessage("Price is required field")
                .GreaterThanOrEqualTo(0).WithMessage("Price cannot be less than 0");
        }
    }
}


