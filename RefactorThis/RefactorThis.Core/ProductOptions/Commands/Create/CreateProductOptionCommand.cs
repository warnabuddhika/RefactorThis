using MediatR;
using RefactorThis.Core.Common.Interfaces;
using RefactorThis.Domain.Models.Products;

namespace RefactorThis.Core.ProductOptions.Commands.Create
{
    public class CreateProductOptionCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
        public bool IsNew { get; }
    }

    public class CreateProductOptionCommandHandler : IRequestHandler<CreateProductOptionCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductOptionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateProductOptionCommand request, CancellationToken cancellationToken)
        {

            Product product = new()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                DeliveryPrice = request.DeliveryPrice,
                IsNew = request.IsNew

            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }
    }
}
