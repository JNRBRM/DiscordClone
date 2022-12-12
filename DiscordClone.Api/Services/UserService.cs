using DiscordClone.Api.Entities;
using DiscordClone.Api.handlers;
using DiscordClone.Api.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;

namespace DiscordClone.Api.Services
{
    public class UserService : GenericService<User,Guid>, IUserService
    {
        private readonly IUserRepository _UserRepository;
        private readonly Passwordhandler _PasswordHandler;
        private readonly JWTSettings _JWTSettings;
        public UserService(IUserRepository UserRepository) : base(UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public async Task<JWT> Login(string Email, string Password)
        {
            User Userdate = await _UserRepository.FindByConditionAsync(obj => obj.Email == Email);

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

        public async override Task<bool> Create(User Item)
        {
            var PasswordHolder = Item.PhoneNumber;

            Item.PhoneNumber = "";
            if (!await base.Create(Item))
            {
                return false;
            }
            var UserHolde = await _UserRepository.FindByConditionAsync(obj => obj.Email == Item.Email);
            return await _PasswordHandler.CreatePassword(PasswordHolder,UserHolde.Id);
        }
    //    public async override Task<bool> Create(JsonObject Item)
    //    {
            
    //    }
    }
}
