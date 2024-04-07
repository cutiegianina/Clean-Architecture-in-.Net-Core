using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
		DbContextEventData eventData,
		InterceptionResult<int> result,
		CancellationToken cancellationToken = default)
	{
		DbContext? dbContext = eventData.Context;

		if (dbContext is null)
			return base.SavingChangesAsync(eventData, result, cancellationToken);

		var entries = dbContext.ChangeTracker.Entries<AuditableEntity>();

		foreach (var entry in entries)
		{
			if (entry.State == EntityState.Added)
				entry.Property(x => x.DateCreated).CurrentValue = DateTime.Now;

			if (entry.State == EntityState.Modified)
				entry.Property(x => x.DateLastModified).CurrentValue = DateTime.Now;
		}

		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}
}
