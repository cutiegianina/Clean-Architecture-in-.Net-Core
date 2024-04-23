using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Common.Interfaces.Data;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<Product> Product { get; set; }
    DbSet<Category> Category { get; set; }
    DbSet<Customer> Customer { get; set; }
    DbSet<User> User { get; set; }
    DbSet<Gender> Gender { get; set; }
    DbSet<Role> Role { get; set; }
}