using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Common.Interfaces;
using RefactorThis.Domain.Models.Products;

namespace RefactorThis.Core.ProductOption.Commands.Update
{
    public class UpdateProductOptionCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
        public bool IsNew { get; }
    }

    public class UpdateProductOptionCommandHandler : IRequestHandler<UpdateProductOptionCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductOptionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(UpdateProductOptionCommand request, CancellationToken cancellationToken)
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
