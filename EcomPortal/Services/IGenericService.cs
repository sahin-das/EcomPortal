namespace EcomPortal.Services
{
    public interface IGenericService<T, TCreateDto, TUpdateDto> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<T> CreateAsync(TCreateDto request);
        Task<T> UpdateAsync(Guid id, TUpdateDto request);
        Task DeleteAsync(Guid id);
    }
}
