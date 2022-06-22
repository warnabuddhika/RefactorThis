using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Common.Interfaces;
using RefactorThis.Core.Common.ViewModels;

namespace RefactorThis.Core.ProductOption.Queries
{
    public class GetProductOptionByNameQuery : IRequest<ProductOptionVm>
    {
        public string Name { get; set; }
    }

    public class GetProductOptionsByNameQueryHandler : IRequestHandler<GetProductOptionByNameQuery, ProductOptionVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductOptionsByNameQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductOptionVm> Handle(GetProductOptionByNameQuery request, CancellationToken cancellationToken)
        {
            var productOption = await _context.ProductOptions.FirstOrDefaultAsync(x => x.Name == request.Name);

            return _mapper.Map<ProductOptionVm>(productOption);    
      
        }
    }
}
