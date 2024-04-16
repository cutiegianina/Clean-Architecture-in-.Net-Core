namespace Application.Common.Interfaces.Data;

public  interface IUnitOfWork<TEntity>
	where TEntity: class
{
	Task<int> CommitAsync(CancellationToken cancellationToken);
	Task AddAsync(TEntity entity, CancellationToken cancellationToken);
	void Update(TEntity entity);
	void Remove(TEntity entity);
}