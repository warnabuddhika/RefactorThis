using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Common.Interfaces;
using RefactorThis.Core.Common.ViewModels;
using AutoMapper.QueryableExtensions;

namespace RefactorThis.Core.Products.Queries
{
    public class GetProductOptionsListQuery : IRequest<IQueryable<ProductOptionVm>>
    {
        public Guid ProductId { get; set; }
    }

    public class GetProductOptionsListQueryHandler : IRequestHandler<GetProductOptionsListQuery, IQueryable<ProductOptionVm>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductOptionsListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<ProductOptionVm>> Handle(GetProductOptionsListQuery request, CancellationToken cancellationToken)
        {
            return _context.ProductOptions.ProjectTo<ProductOptionVm>(_mapper.ConfigurationProvider).AsQueryable();
        }
    }
}
