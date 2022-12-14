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

namespace DiscordClone.Api.Services
{
    public class UserService : GenericService<User,Guid>, IUserService
    {
        private const string _RegisterCacheKey = "RegisterCache";
        private readonly IUserRepository _UserRepository;
        private readonly IMemoryCache _Cache;
        private readonly Passwordhandler _PasswordHandler;
        private readonly RegisterHandler _RegisterHandler;
        private readonly JWTSettings _JWTSettings;
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
            }
            return true;
        }

        public async Task<JWT> Login(string Email, string Password)
        {
            User Userdate = await _UserRepository.FindByCondition(obj => obj.Email == Email);

            if (!await _PasswordHandler.checkPassword(Password,Userdate.Id))
            {
                return null;
            }

            //  måske lave en klasse til at håndter token \\
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_JWTSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", Userdate.Id.ToString()),
                    new Claim("Email", Userdate.Email), //ved ikke om vi skal bruge email
                    new Claim("AccountId", Userdate.AccountId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JWT() { Token = tokenHandler.WriteToken(token) };
        }
        public async Task<bool> Register(RegisterModel Register)
        {
            EmailHandler handler = new();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(86400))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);
            var uniqueId = Guid.NewGuid();
            if (_Cache.TryGetValue(_RegisterCacheKey, out List<CachedItem<RegisterModel, Guid>> registers))
            {
                var attempt = registers.FirstOrDefault(x => x.Item.Email == Register.Email);
                if (attempt==null)
                {
                    registers.Add(new CachedItem<RegisterModel, Guid>() { Id=uniqueId,Item=Register});
                }
            }
            else
            {
                List<CachedItem<RegisterModel, Guid>> currentRegisters = new();
                currentRegisters.Add(new CachedItem<RegisterModel, Guid> { Id = uniqueId, Item = Register });
               _Cache.Set(_RegisterCacheKey, currentRegisters, cacheEntryOptions);
            }
            var response=await handler.SendEmail(Register.Email,$"<a href=\"https://localhost:44329/api/User/activate/{uniqueId}\">hej</a>");
            return true;
        }

        //hej2@lortemail.dk
    }
    public class RegisterHandler
    {
        private readonly UserService _UserService;
        public async Task<User> UserMapping(string Email,string? PhoneNumber)
        {
            User user = new User
            {
                Email = Email,
                PhoneNumber = PhoneNumber,
                PasswordSetDate = DateTime.Now,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false
            };
            return null;
        }

        public async Task<Account> AccountMapping(string Displayname,Guid id)
        {
            return new Account
            {
                UserId = id,
                DisplayName = Displayname,
                CreatedDate = DateTime.Now,
                LastLogonDate = DateTime.Now,
            };
        }

    }
}
