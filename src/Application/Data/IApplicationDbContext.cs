using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data;

public interface IApplicationDbContext
{
	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	public DbSet<Product> Product { get; set; }
	public DbSet<Category> Category { get; set; }
	public DbSet<Customer> Customer { get; set; }
}
