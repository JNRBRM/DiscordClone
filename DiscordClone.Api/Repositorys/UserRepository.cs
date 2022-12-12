using DiscordClone.Api.DBContext;
using DiscordClone.Api.Entities;
using DiscordClone.Api.Interface;

namespace DiscordClone.Api.Repositorys
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private DiscordCloneContext _Context;
        public UserRepository(DiscordCloneContext Context) : base(Context)
        {
            _Context= Context;
        }
        public Task<bool> Login(string Email)
        {
            throw new NotImplementedException();
        }

    }
}
