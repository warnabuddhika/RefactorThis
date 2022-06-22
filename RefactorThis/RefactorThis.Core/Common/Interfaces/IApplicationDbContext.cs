using Microsoft.EntityFrameworkCore;
using RefactorThis.Domain.Models.Products;

namespace RefactorThis.Core.Common.Interfaces;
public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    void SetEntryState<TEntry>(TEntry entry, EntityState entityState);
    DbSet<TModel> Set<TModel>() where TModel : class;       

    public DbSet<Product> Products { get; set; }



}
