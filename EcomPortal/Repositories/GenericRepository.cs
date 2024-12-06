using EcomPortal.Data;
using Microsoft.EntityFrameworkCore;

namespace EcomPortal.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T? GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public T Add(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public T Update(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"Entity with ID {id} not found.");
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                throw new ArgumentException($"Entity with ID {id} not found.");
            }

            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
