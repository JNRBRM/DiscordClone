using System.Linq.Expressions;

namespace DiscordClone.Api.Interface
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAll();

        Task<T> GetById(Expression<Func<T, bool>> predicate);

        Task<T> Update(T Item);

        Task<T> Create(T Item);

        Task<bool> Delete(Expression<Func<T, bool>> predicate);

        Task<T> FindByCondition(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindByConditionToList(Expression<Func<T, bool>> predicate);
    }
}
