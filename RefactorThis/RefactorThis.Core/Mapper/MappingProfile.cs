using AutoMapper;
using RefactorThis.Core.Common.ViewModels;
using RefactorThis.Domain.Models.Products;

namespace RefactorThis.Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductVm>().ReverseMap();
        }
    }
}
