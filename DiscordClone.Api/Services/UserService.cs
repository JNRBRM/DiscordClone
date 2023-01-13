using DiscordClone.Api.DataModels;
using DiscordClone.Api.Entities;
using DiscordClone.Api.handlers;
using DiscordClone.Api.Interface;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml;

namespace DiscordClone.Api.Services
{
    public class UserService : GenericService<User,Guid>, IUserService
    {
        private const string _RegisterCacheKey = "RegisterCache";
        private readonly IUserRepository _UserRepository;
        private readonly IMemoryCache _Cache;
        private readonly Passwordhandler _PasswordHandler;
        private readonly JWTSettings _JWTSettings;
        private readonly JWTHandler jWTHandler = new();
        public UserService(IUserRepository UserRepository, IOptions<JWTSettings> jwtSettings, IMemoryCache cache) : base(UserRepository)
        {
            _UserRepository = UserRepository;
            _Cache = cache;
            _JWTSettings = jwtSettings.Value;
        }

        public async Task<object?> Activate(Guid token)
        {
            if (_Cache.TryGetValue(_RegisterCacheKey, out IEnumerable<CachedItem<RegisterModel, Guid>> registers))
            {
                var attempt = registers.FirstOrDefault(x => x.Id == token);
                if (attempt != null)
                {

                    User user = new()
                    {
                        Email = attempt.Item.Email,
                        PhoneNumber = attempt.Item.PhoneNumber,
                        Account = new()
                        {
                            DisplayName=attempt.Item.Displayname,

                        },
                        
                    };
                    try
                    {
                        var res = await _UserRepository.Create(user);
                        await _PasswordHandler.CreatePassword(attempt.Item.Password, res.Id);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
           
            return true;
        }

        public async Task<JWT> Login(string Email, string Password)
        {
            //User Userdate = await _UserRepository.FindByCondition(obj => obj.Email == Email);

            // test data \\
            User Userdate = new User
            {
                Email = Email,
                PhoneNumber = Password,
                Account = new() { },
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

            };
            
            //if (!await _PasswordHandler.checkPassword(Password,Userdate.Id))
            //{
            //    return null;
            //}
            return await jWTHandler.CreateJwt<User>(Userdate, _JWTSettings.Secret);
        }
        public async Task<bool> Register(RegisterModel Register)
        {
            EmailHandler handler = new();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(86400))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(2);

            var uniqueId = Guid.NewGuid();

            if (_Cache.TryGetValue(_RegisterCacheKey, out List<CachedItem<RegisterModel, Guid>> registers))
            {
                var attempt = registers.FirstOrDefault(x => x.Item.Email == Register.Email);
                if (attempt == null)
                {
                    registers.Add(new CachedItem<RegisterModel, Guid>() { Id = uniqueId, Item = Register });
                }
            }
            else
            {
                List<CachedItem<RegisterModel, Guid>> currentRegisters = new();
                currentRegisters.Add(new CachedItem<RegisterModel, Guid> { Id = uniqueId, Item = Register });
                _Cache.Set(_RegisterCacheKey, currentRegisters, cacheEntryOptions);
            }

            //var response=await handler.SendEmail(Register.Email,$"<a href=\"https://localhost:44329/api/User/activate/{uniqueId}\">hej</a>");
            //var holder = jWTHandler.CreateJwt(Register, _JWTSettings.Secret);
            return true;
        }
        //4e2f49d9-e93e-435e-96bf-9fb4dbe32f43
        //hej2@lortemail.dk
    }
}
