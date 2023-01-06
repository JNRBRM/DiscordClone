using DiscordClone.Api.DataModels;
using DiscordClone.Api.Entities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Security;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace DiscordClone.Api.handlers
{
    public class JWTHandler
    {
        private readonly JWTSettings _jwtSettings;
        private readonly JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        public async Task<JWT> CreateJwt<T>(T obj,string SECRET_KEY)
        {
            // Get the object's properties
            var properties = obj.GetType().GetProperties().Where(p=>p.Name!= "Id");
            // Create a dictionary of the object's properties and their values
            Dictionary<string, object> payload = new Dictionary<string, object>();
            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                if (!payload.ContainsKey(property.Name))
                {
                    payload.Add(property.Name, property.GetValue(obj));
                }
            }
            //var payload = properties.ToDictionary(p =>
            //{
            //    Dictionary<string,object> holder = new();

            //    if (!holder.ContainsKey(p.Name))
            //    {
            //        holder.Add(p.Name, p.GetValue(obj));
            //    }
            //    return holder;
            //});
            //var payload2 = properties.ToDictionary(p => p.Name, p => p.GetValue(obj));
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
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)),
                algorithm.ToString())
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = new JWT { Token = tokenHandler.WriteToken(token) };
            var holder = Decoder<User>(jwt.Token);
            return jwt;
            //return new JWT{ Token = tokenHandler.WriteToken(token) };
        }




        public T Decoder<T>(string jwt) where T : new()
        {
            JwtSecurityToken token = tokenHandler.ReadJwtToken(jwt);
            T obj = new T();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var claim = token.Claims.FirstOrDefault(c => c.Type == property.Name);
                if (claim == null) continue;

                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    // Property is a custom object, recurse
                    continue;
                    //var nestedObject = DecodeJwt(claim.Value, property.PropertyType);
                    //property.SetValue(obj, nestedObject);
                }
                else if (property.PropertyType == typeof(Guid))
                {
                    // Property is a Guid, parse it
                    var value = Guid.Parse(claim.Value);
                    property.SetValue(obj, value);
                }
                else
                {
                    var value = Convert.ChangeType(claim.Value, property.PropertyType);
                    property.SetValue(obj, value);
                }
            }

            return obj;
        }
        private object DecodeJwt(string jwt, Type type)
        {
            JwtSecurityToken token = tokenHandler.ReadJwtToken(jwt);

            var obj = Activator.CreateInstance(type);
            foreach (var property in type.GetProperties())
            {
                var claim = token.Claims.FirstOrDefault(c => c.Type == property.Name.ToLower());
                if (claim != null)
                {
                    if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                    {
                        // Property is a custom object, recurse
                        var nestedObject = DecodeJwt(claim.Value, property.PropertyType);
                        property.SetValue(obj, nestedObject);
                    }
                    else
                    {
                        property.SetValue(obj, claim.Value);
                    }
                }
            }

            return obj;
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