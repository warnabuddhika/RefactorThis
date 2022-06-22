using AutoMapper;
using RefactorThis.Core.Common.Mappings;
using RefactorThis.Domain.Models.Products;

namespace RefactorThis.Core.Projects.Queries;

public class ProductVM:IMapFrom<Product>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductVM>();

    }
}
