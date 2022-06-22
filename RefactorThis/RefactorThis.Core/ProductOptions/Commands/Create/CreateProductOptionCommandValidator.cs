using FluentValidation;

namespace RefactorThis.Core.ProductOptions.Commands.Create
{
    public class CreateProductOptionCommandValidator : AbstractValidator<CreateProductOptionCommand>
    {
        public CreateProductOptionCommandValidator()
        {            
            RuleFor(v => v.Name).NotEmpty().WithMessage("Product name is required");
            
        }
    }
}
