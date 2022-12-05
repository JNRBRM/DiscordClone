﻿using System.Linq.Expressions;

namespace DiscordClone.Api.Interface
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAll();

        Task<T> GetById(Type id);

       // Task<T> Update(Type Id, T Item);
        Task<T> Update(T Item);

        Task<bool> Create(T Item);

        Task<bool> Delete(Type id);

        Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindByConditionToList(Expression<Func<T, bool>> predicate);
    }
}
