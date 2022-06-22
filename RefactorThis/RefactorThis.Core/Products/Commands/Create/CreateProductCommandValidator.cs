using FluentValidation;

namespace RefactorThis.Core.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {            
            RuleFor(v => v.Name).NotEmpty().WithMessage("Product name is required");
            
        }
    }
}
