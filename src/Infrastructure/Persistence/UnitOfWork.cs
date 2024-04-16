using Application.Common.Interfaces.Data;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UnitOfWork<TEntity> : IUnitOfWork<TEntity>
    where TEntity : class
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _entity;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        _entity = _context.Set<TEntity>();
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken) =>
        await _entity.AddAsync(entity, cancellationToken);

    public void Update(TEntity entity) =>
        _entity.Update(entity);

    public void Remove(TEntity entity) =>
        _entity.Remove(entity);
}