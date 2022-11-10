using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Base;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext context)
        {
            this.context = context;
        }
        public async Task<T> GetByIdAsync(int Id)
        {
            return await context.Set<T>().FindAsync(Id);
        }
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }
        public async Task<T> GetEntityWithSpec(ISpecification<T> specification)
        {
          return await ApplySpecification(specification).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> ListAysnc(ISpecification<T> specification)
        {
            return await  ApplySpecification(specification).ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), specification);
        }
    }
}