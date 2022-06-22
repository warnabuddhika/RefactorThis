using FluentValidation;

namespace RefactorThis.Core.Products.Commands.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {            
            RuleFor(v => v.Name).NotEmpty().WithMessage("Product name is required");
            
        }
    }
}
