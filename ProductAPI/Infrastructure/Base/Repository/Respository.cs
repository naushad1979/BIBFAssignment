using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Infrastructure.Base.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> SaveAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> SaveAllAsync(List<T> entities)
        {
            await _dbContext.AddRangeAsync(entities);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await FindByIdAsync(id);
            return entity != null;
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<T> FindByNameAsync(string name)
        {
            return await _dbContext.Set<T>().FindAsync(name);
        }

        public async Task<IReadOnlyList<T>> FindAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _dbContext.Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            int result =await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<T> FindByMultiCriteriaAsync(Func<T, bool> where, params Expression<Func<T,
            object>>[] navigationProperties)
        {

            IQueryable<T> dbQuery = _dbContext.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            var result = dbQuery.Where(where).SingleOrDefault();

            return result;
        }

         
    }
}
