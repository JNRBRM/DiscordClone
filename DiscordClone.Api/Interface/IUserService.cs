using DiscordClone.Api.DataModels;
using DiscordClone.Api.Entities;
using Microsoft.Win32;
using System.Text.Json.Nodes;

namespace DiscordClone.Api.Interface
{
    public interface IUserService : IGenericService<User,Guid>
    {
        Task<object?> Activate(Guid token);
        Task<JWT> Login(string Email,string Password);

        Task<bool> Register(RegisterModel Register);
    }
}
