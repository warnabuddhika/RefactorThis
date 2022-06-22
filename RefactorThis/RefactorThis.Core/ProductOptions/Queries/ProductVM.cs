using AutoMapper;
using RefactorThis.Core.Common.Mappings;
using RefactorThis.Domain.Models.Products;

namespace RefactorThis.Core.Products.Queries;

public class ProductVM:IMapFrom<Product>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string Description { get; set; }

    public decimal Price { get; set; }

    public decimal DeliveryPrice { get; set; }
    public bool IsNew { get; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductVM>().ReverseMap();

    }
}
