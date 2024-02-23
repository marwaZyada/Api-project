using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _dbcontext;

		public GenericRepository(StoreContext dbcontext)
        {
			_dbcontext = dbcontext;
		}
        public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			
				return await _dbcontext.Set<T>().ToListAsync();
		
	}

    

        public async Task<T> GetByIdAsync(int? id)
		{
			
			
				return await _dbcontext.Set<T>().FindAsync(id);
			
			
		}



        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> Spec)
        {
         return await ApplyQuery(Spec).ToListAsync();
        }
        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplyQuery(Spec).FirstOrDefaultAsync();
        }
        public async Task<int> GetCountWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplyQuery(Spec).CountAsync();
        }
        private IQueryable<T> ApplyQuery(ISpecification<T> Spec)
		{
			return  SpacificationEvaluator<T>.GetQuery(_dbcontext.Set<T>(), Spec);

        }

        public async Task Add(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
        }
    }
}
