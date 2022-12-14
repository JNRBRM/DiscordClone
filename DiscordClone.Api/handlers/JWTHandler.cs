using DiscordClone.Api.DataModels;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DiscordClone.Api.handlers
{
    public class JWTHandler
    {
        public async Task<JWT> CreateJwt<T>(T obj,string SECRET_KEY)
        {
            // Get the object's properties
            var properties = obj.GetType().GetProperties();

            // Create a dictionary of the object's properties and their values
            var payload = properties.ToDictionary(p => p.Name, p => p.GetValue(obj, null));

            // Use the dictionary to create a JWT
            return Encode(payload, SECRET_KEY, JwtHashAlgorithm.HS256);
        }

        public JWT Encode<TKey, TValue>(Dictionary<TKey, TValue> load,string Secret, JwtHashAlgorithm algorithm)
        {
            List<Claim> claims = new();
            foreach (var (key,value) in load)
            {
                claims.Add(new Claim(key.ToString(), value.ToString()));
            }

            return new JWT(); //{ Token = tokenHandler.WriteToken(token) };
        }

    }
}
/*
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

 */