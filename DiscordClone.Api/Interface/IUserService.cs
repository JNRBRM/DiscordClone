using DiscordClone.Api.Entities;
using System.Text.Json.Nodes;

namespace DiscordClone.Api.Interface
{
    public interface IUserService : IGenericService<User>
    {
        Task<JWT> Login(string Email,string Password);
    }
}
