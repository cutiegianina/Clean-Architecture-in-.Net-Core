using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;

namespace Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{

	private readonly IHttpContextAccessor _contextAccessor;

	public AuditableEntityInterceptor(IHttpContextAccessor contextAccessor) =>
		_contextAccessor = contextAccessor;

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
		DbContextEventData eventData,
		InterceptionResult<int> result,
		CancellationToken cancellationToken = default)
	{
		DbContext? dbContext = eventData.Context;

		if (dbContext is null)
			return base.SavingChangesAsync(eventData, result, cancellationToken);

		IEnumerable<Claim>? claims = _contextAccessor?.HttpContext?.User?.Claims;
		var userId = claims?.FirstOrDefault(claim => claim.Type == JwtTokenClaims.UserId)?.Value;

		var entries = dbContext.ChangeTracker.Entries<AuditableEntity>();

		foreach (var entry in entries)
		{
			if (entry.State == EntityState.Added)
			{
				entry.Property(x => x.CreatedBy).CurrentValue = userId;
				entry.Property(x => x.DateCreated).CurrentValue = DateTime.Now;
			}

			if (entry.State == EntityState.Modified)
			{
				entry.Property(x => x.LastModifiedBy).CurrentValue = userId;
				entry.Property(x => x.DateLastModified).CurrentValue = DateTime.Now;
			}
		}

		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}
}
