using Microsoft.EntityFrameworkCore;
using SupportChat.Data.Abstract;
using SupportChat.Data.Context;

namespace SupportChat.Data.Implementation
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Delete(T entity)
        {
            _applicationDbContext.Set<T>().Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<T> Get(int id)
        {
            return await _applicationDbContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> Insert(T entity)
        {
            _applicationDbContext.Set<T>().Add(entity);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IList<T>> List()
        {
            return await _applicationDbContext.Set<T>().ToListAsync();
        }

        public async Task<bool> SoftDelete(T entity)
        {
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(T entity)
        {
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
