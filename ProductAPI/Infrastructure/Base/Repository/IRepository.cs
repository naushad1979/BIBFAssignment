using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Infrastructure.Base.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> FindByIdAsync(int id);
        Task<T> FindByNameAsync(string name);
        public Task<T> FindByMultiCriteriaAsync(Func<T, bool> where, params Expression<Func<T,
                    object>>[] navigationProperties);
        Task<IReadOnlyList<T>> FindAllAsync();
        Task<bool> ExistsAsync(int id);
        //Task<T> Add(T entity);
        Task<int> SaveAsync(T entity);
        Task<int> SaveAllAsync(List<T> entities);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
    }
}
