using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Common.Interfaces;
using RefactorThis.Domain.Models;
using RefactorThis.Domain.Models.Products;
using RefactorThis.Infrastructure.Persistence.Configurations.Products;
using RefactorThis.Infrastructure.Persistence.Extensions;
using System.Reflection;

namespace RefactorThis.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private static DbContextHelper _helper = new();
        private readonly IAuditDbContext _auditContext;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _auditContext = new DefaultAuditContext(this);
            _helper.SetConfig(_auditContext);
        }

        public override int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in ChangeTracker.Entries<ModelBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedOn = DateTime.Now;
                        break;
                }
            }

            return await _helper.SaveChangesAsync(_auditContext, () => base.SaveChangesAsync(cancellationToken));
        }
        public void SetEntryState<TEntry>(TEntry entry, EntityState entityState)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            this.Entry(entry).State = entityState;
#pragma warning restore CS8604 // Possible null reference argument.
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Seed();
            this.Configure(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }

        private void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductOptionConfiguration());
        }
    }
}
