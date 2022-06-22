using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Common.Interfaces;
using RefactorThis.Domain.Models.Products;

namespace RefactorThis.Core.Products.Commands.Update
{
    public class DeleteProductOptionCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
       
    }

    public class DeleteProductOptionCommandHandler : IRequestHandler<DeleteProductOptionCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProductOptionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteProductOptionCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);

            if(product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
