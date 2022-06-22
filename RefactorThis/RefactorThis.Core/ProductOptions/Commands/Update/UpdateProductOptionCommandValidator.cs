using FluentValidation;
using RefactorThis.Core.ProductOption.Commands.Update;

namespace RefactorThis.Core.ProductOptions.Commands.Update
{
    public class UpdateProductOptionCommandValidator : AbstractValidator<UpdateProductOptionCommand>
    {
        public UpdateProductOptionCommandValidator()
        {            
            RuleFor(v => v.Name).NotEmpty().WithMessage("Product option name is required");
            
        }
    }
}
