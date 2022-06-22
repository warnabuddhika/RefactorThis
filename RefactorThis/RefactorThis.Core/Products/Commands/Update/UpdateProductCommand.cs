using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Common.Interfaces;
using RefactorThis.Domain.Models.Products;

namespace RefactorThis.Core.Products.Commands.Update
{
    public class UpdateProductCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
        public bool IsNew { get; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);

            if(product == null)
            {
                return Guid.Empty;
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.DeliveryPrice = request.DeliveryPrice;
            product.IsNew = request.IsNew;


            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }
    }
}
