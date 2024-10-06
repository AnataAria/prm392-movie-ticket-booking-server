using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayers
{
    public class GenericRepository<T>(Prn221projectContext context) where T : class
    {
        protected readonly Prn221projectContext _context = context;

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> FindOneAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            var enumerable = entities as T[] ?? entities.ToArray();
            if (enumerable.Any())
            {
                _context.Set<T>().RemoveRange(enumerable);
                await _context.AddRangeAsync();
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            var enumerable = entities as T[] ?? entities.ToArray();
            if (enumerable.Any())
            {
                _context.Set<T>().UpdateRange(enumerable);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id)
        {
            var entity =  await GetByIdAsync(id);
            return entity != null;
        }

        public async Task<T?> FindOneWithNoTrackingAsync(Expression<Func<T, bool>> expression)
        {
            var dbObject = await _context.Set<T>().Where(expression).AsNoTracking().FirstOrDefaultAsync();
            return dbObject;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        //public static implicit operator GenericRepository<T>(TransactionTypeRepository v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
