using DiscordClone.Api.Entities;

namespace DiscordClone.Api.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> Login(string Email);
    }
}
