using MediatR;
using RefactorThis.Core.Common.Interfaces;
using RefactorThis.Domain.Models.Products;

namespace RefactorThis.Core.Products.Commands.Create
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
        public bool IsNew { get; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
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
