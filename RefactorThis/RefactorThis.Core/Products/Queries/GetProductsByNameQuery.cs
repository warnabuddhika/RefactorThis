using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Common.Interfaces;
using RefactorThis.Core.Common.ViewModels;

namespace RefactorThis.Core.Products.Queries
{
    public class GetProductsByNameQuery : IRequest<ProductVm>
    {
        public string Name { get; set; }
    }

    public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, ProductVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsByNameQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductVm> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == request.Name);

            return _mapper.Map<ProductVm>(product);    
      
        }
    }
}
