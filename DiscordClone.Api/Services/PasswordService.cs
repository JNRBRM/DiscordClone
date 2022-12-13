﻿using DiscordClone.Api.Entities;
using DiscordClone.Api.Interface;
using System.Linq.Expressions;

namespace DiscordClone.Api.Services
{
    public class PasswordService:GenericService<SecurityCredentials, Guid>
    {
        public PasswordService(IGenericRepository<SecurityCredentials> GenericRepository):base(GenericRepository) 
        {

        }

        public override Task<SecurityCredentials> FindByCondition(Expression<Func<SecurityCredentials, bool>> predicate)
        {
            return base.FindByCondition(predicate);
        }
    }
}
