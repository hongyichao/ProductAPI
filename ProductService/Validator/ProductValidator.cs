using FluentValidation;
using ProductBusiness.Dtos;
using ProductEntity;

namespace ProductBusiness.Validator
{
	public class ProductValidator : AbstractValidator<ProductDto>
	{
		public ProductValidator()
		{
			RuleFor(x => x.Id)
				.NotNull().NotEmpty().WithMessage("Id is required field")
				.Length(1,100).WithMessage("The valid length of Name field is 1 to 100")
				.Matches("^[a-zA-Z][a-zA-Z0-9]*$").WithMessage("Id can only have alphanumeric characters");
			RuleFor(x => x.Name)
				.NotNull().WithMessage("Name is required field")
				.Length(1, 500).WithMessage("The valid length of Name field is 1 to 500");			
			RuleFor(x => x.Price)
				.NotNull().WithMessage("Price is required field")
				.GreaterThanOrEqualTo(0).WithMessage("Price cannot be less than 0");
		}
	}
}


