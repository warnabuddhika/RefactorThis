using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Common.Interfaces;
using RefactorThis.Core.Common.ViewModels;

namespace RefactorThis.Core.ProductOptions.Queries
{
    public class GetProductOptionsQuery : IRequest<ProductOptionVm>
    {
        public Guid ProductId { get; set; }
        public Guid Id { get; set; }
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductOptionsQuery, ProductOptionVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductOptionVm> Handle(GetProductOptionsQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.ProductOptions.FirstOrDefaultAsync(x => x.Id == request.ProductId);

            return _mapper.Map<ProductOptionVm>(product);

           


        }
    }
}
