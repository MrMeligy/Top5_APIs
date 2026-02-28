using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Top5.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity != null;
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                         .FirstOrDefaultAsync(predicate);
        }
        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                         .CountAsync(predicate);
        }
        public async Task<int> DeleteManyAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _context.Set<T>()
                .Where(predicate)
                .ExecuteDeleteAsync();
            }
            catch (Exception ex) {
                throw;
            }
            
        }
    }
}

