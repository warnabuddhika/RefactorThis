using FluentValidation;
using RefactorThis.Core.Products.Commands;

namespace RefactorThis.Core.Projects.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {         

            RuleFor(v => v.Name)
                .NotEmpty();

            //RuleFor(v => v.StartDate)
            //    .NotEmpty();

            //RuleFor(v => v.IsActive)
            //    .NotEmpty();
            //RuleFor(v => v.CompanyId).GreaterThan(1);
        }
    }
}
