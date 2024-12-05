using EcomPortal.Repositories;

namespace EcomPortal.Services
{
    public class GenericService<T, TCreateDto, TUpdateDto>(IGenericRepository<T> repository) 
        : IGenericService<T, TCreateDto, TUpdateDto> where T : class
    {
        private readonly IGenericRepository<T> _repository = repository;

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<T> CreateAsync(TCreateDto request)
        {
            var entity = MapToEntity(request);
            await _repository.AddAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(Guid id, TUpdateDto request)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new ArgumentException("Entity not found.");
            }

            MapToEntity(request, entity);

            await _repository.UpdateAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public virtual T MapToEntity(TCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public virtual void MapToEntity(TUpdateDto dto, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
