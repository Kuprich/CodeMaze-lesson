using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly RepositoryContext _repositoryContext;

    public RepositoryBase(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }
    public void Create(T entity) =>
        _repositoryContext.Set<T>().Add(entity);

    public void Delete(T entity) =>
        _repositoryContext.Set<T>().Remove(entity);

    public void Update(T entity) =>
        _repositoryContext.Set<T>().Update(entity);

    public IQueryable<T> FindAll(bool trackChanges = true) =>
        trackChanges
            ? _repositoryContext.Set<T>()
            : _repositoryContext.Set<T>().AsNoTracking();


    public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool trackChanges = true) =>
        trackChanges
            ? _repositoryContext.Set<T>().Where(expression)
            : _repositoryContext.Set<T>().Where(expression).AsNoTracking();

}
