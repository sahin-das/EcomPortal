namespace EcomPortal.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(Guid id);

        IEnumerable<T> GetAll();
        T? GetById(Guid id);
        T Add(T entity);
        T Update(T entity);
        void Delete(Guid id);
    }
}
