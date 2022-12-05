﻿using DiscordClone.Api.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DiscordClone.Api.Interface
{
    public interface IGenericService<T>
    {
        Task<List<T>> GetAll();

        Task<T> GetById(Type id);

        Task<T> Update(T Item);

        Task<bool> Create(T Item);

        Task<bool> Delete(Type id);

        Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindByConditionToList(Expression<Func<T, bool>> predicate);
    }
}
