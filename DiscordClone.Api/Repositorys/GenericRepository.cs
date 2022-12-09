using DiscordClone.Api.DBContext;
using DiscordClone.Api.Entities;
using DiscordClone.Api.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DiscordClone.Api.Repositorys
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DiscordCloneContext _Context;

        public GenericRepository(DiscordCloneContext Context)
        {
            _Context = Context;
        }
        private IQueryable<T> GetEntity()
        {
            try
            {
                return _Context.Set<T>();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> Create(T Item)
        {
            //_Context.Add(Item);
            //await _Context.SaveChangesAsync();
            return true;
        }

        public async virtual Task<bool> Delete(Type id)
        {
            try
            {
                var Entity = await GetById(id);
                _Context.Remove(Entity);
                await _Context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        

        public Task<List<T>> GetAll()
        {
           return GetEntity().ToListAsync();
        }

        public async Task<T> GetById(Type id)
        {
            return await GetEntity().SingleOrDefaultAsync(obj => obj.Id == id);
        }

        public async virtual Task<T> Update(T Item)
        {
            _Context.Entry(Item).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
            return Item;
        }
        public async virtual Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetEntity().FirstOrDefaultAsync(predicate);
        }

        public async virtual Task<List<T>> FindByConditionToList(Expression<Func<T, bool>> predicate)
        {
            return await GetEntity().Where(predicate).ToListAsync();
        }
    }
}
