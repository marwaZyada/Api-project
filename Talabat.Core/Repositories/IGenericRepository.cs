using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repository
{
	public interface IGenericRepository<T>where T:BaseEntity
	{
		Task<IReadOnlyList<T>> GetAllAsync();
		Task<T> GetByIdAsync(int? id);

        #region function with specification
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> Spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec);

        Task<int> GetCountWithSpecAsync(ISpecification<T> Spec);
        Task Add(T entity);

        void Update(T entity);
        void Delete(T entity);
        #endregion
    }
}
