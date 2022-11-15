using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Base;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int Id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> specification);
        Task<IReadOnlyList<T>> ListAysnc(ISpecification<T> specification);
        Task<int> CountAsync(ISpecification<T> specification);
    }
}