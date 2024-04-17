namespace Domain.Abstractions;

public  interface IUnitOfWork<TEntity>
	where TEntity: class
{
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	Task AddAsync(TEntity entity, CancellationToken cancellationToken);
	void Update(TEntity entity);
	void Remove(TEntity entity);
}