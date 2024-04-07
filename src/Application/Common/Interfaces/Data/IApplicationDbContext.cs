using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Common.Interfaces.Data;

public interface IApplicationDbContext
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Customer> Customer { get; set; }
}