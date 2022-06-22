using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RefactorThis.Core.Common.Interfaces;

namespace RefactorThis.Core.Projects.Queries
{
    public class GetProductsQuery : IRequest<ProductVM>
    {
        public Guid ProjectId { get; set; }
    }

    public class GetProjectsQueryHandler : IRequestHandler<GetProductsQuery, ProductVM>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductVM> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProjectId);

            //var projectVM = _mapper.Map<ProjectVM>(project);
            var projectVM = new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                //StartDate = project.StartDate,
                //EndDate = project.EndDate,
                //IsActive = project.IsActive
            };

            return projectVM;
                  
        }
    }
}
