using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Common.Interfaces;
using RefactorThis.Core.Common.ViewModels;
using AutoMapper.QueryableExtensions;

namespace RefactorThis.Core.Products.Queries
{
    public class GetProductsListQuery : IRequest<IQueryable<ProductVm>>
    {
        public Guid ProductId { get; set; }
    }

    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, IQueryable<ProductVm>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<ProductVm>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            return _context.Products.ProjectTo<ProductVm>(_mapper.ConfigurationProvider).AsQueryable();    
        }
    }
}
