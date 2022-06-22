using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Common.Interfaces;
using RefactorThis.Core.Common.ViewModels;

namespace RefactorThis.Core.Products.Queries
{
    public class GetProductsQuery : IRequest<ProductVm>
    {
        public Guid ProductId { get; set; }
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductVm> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId);

            return _mapper.Map<ProductVm>(product);

           


        }
    }
}
