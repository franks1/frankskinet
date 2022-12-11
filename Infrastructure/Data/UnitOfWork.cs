using System.Collections;
using Core.Entities.Base;
using Core.Interfaces;
using Infrastructure.Data.Repository;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreContext _db;
    private Hashtable repositories;

    public UnitOfWork(StoreContext db)
    {
        _db = db;
    }
    public void Dispose()
    {
        this._db.Dispose();
    }
    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        repositories = repositories ?? new Hashtable();
        var type = typeof(TEntity).Name;
        // if (repositories.ContainsKey(type))
        // {
        //     return repositories[type] as IGenericRepository<TEntity>;
        // }
        // IGenericRepository<TEntity> repo = new GenericRepository<TEntity>(this._db);
        // repositories.Add(typeof(TEntity), repo);
        // return repo;
        
        
        if (!repositories.ContainsKey(type))
        {
            var repositoryType = typeof(IGenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(typeof(GenericRepository<TEntity>),_db);
            repositories.Add(type, repositoryInstance);
        }
        return (IGenericRepository<TEntity>)repositories[type];
    }
    public async Task<int> Complete()
    {
      return await this._db.SaveChangesAsync();
    }
}