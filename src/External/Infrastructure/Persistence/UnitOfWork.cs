using Domain.Abstractions;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UnitOfWork<TEntity> : IUnitOfWork<TEntity>
    where TEntity : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _entity;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        _entity = _context.Set<TEntity>();
    }

	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        await _entity.AddAsync(entity, cancellationToken);

    public void Update(TEntity entity) =>
        _entity.Update(entity);

    public void Remove(TEntity entity) =>
        _entity.Remove(entity);
}