using eCommerce.BusinessLogicLayer.DTO;
using FluentValidation;

namespace eCommerce.BusinessLogicLayer.Validators;

public class ProductUpdateRequestValidators : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateRequestValidators()
    {
        RuleFor(temp => temp.ProductID).NotEmpty().WithMessage("Product ID is required.");
        RuleFor(temp => temp.ProductName).NotEmpty().WithMessage("Product name is required.");
        RuleFor(temp => temp.Category).IsInEnum().WithMessage("Invalid category.");
        RuleFor(temp => temp.UnitPrice).InclusiveBetween(0, double.MaxValue).WithMessage("Unit price must be greater than or equal to 0.");
        RuleFor(temp => temp.QuantityInStock).InclusiveBetween(0, int.MaxValue).WithMessage("Quantity in stock must be greater than or equal to 0.");
    }
}

