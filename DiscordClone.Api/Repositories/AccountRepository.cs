using DiscordClone.Api.DBContext;
using DiscordClone.Api.Entities;
using DiscordClone.Api.Interface;

namespace DiscordClone.Api.Repositorys
{
    public class AccountRepository:GenericRepository<Account>,IAccountRepository
    {
        private DiscordCloneContext _Context;
        public AccountRepository(DiscordCloneContext Context) : base(Context)
        {
            _Context = Context;
        }
    }
}
