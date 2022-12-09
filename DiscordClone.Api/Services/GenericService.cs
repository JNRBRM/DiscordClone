using DiscordClone.Api.Entities;
using DiscordClone.Api.Interface;
using System.Linq.Expressions;

namespace DiscordClone.Api.Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseEntity
    {
        private IGenericRepository<T> _GenericRepository;

        public GenericService(IGenericRepository<T> GenericRepository)
        {
            _GenericRepository = GenericRepository;
        }

        public async virtual Task<bool> Create(T Item)
        {
            if (await _GenericRepository.Create(Item))
            {
                return true;
            }
            return false;
        }

        public async virtual Task<bool> Delete(Type id)
        {
            return await _GenericRepository.Delete(id);
        }

        public async virtual Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _GenericRepository.FindByConditionAsync(predicate);
        }

        public Task<List<T>> FindByConditionToList(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAll()
        {
            return await _GenericRepository.GetAll();
        }

        public async Task<T> GetById(Type id)
        {
            return await _GenericRepository.GetById(id);
        }

        public async virtual Task<T> Update(T Item)
        {
            return await _GenericRepository.Update(Item);
        }
    }
}
