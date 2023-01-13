using DiscordClone.Api.DataModels;
using DiscordClone.Api.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Security;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DiscordClone.Api.handlers
{
    public class JWTHandler
    {
        private readonly JWTSettings _jwtSettings;
        private readonly JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        public async Task<JWT> CreateJwt<T>(T obj, string SECRET_KEY)
        {
            // Get the object's properties
            var properties = obj.GetType().GetProperties().SkipLast(1);//.Where(p=>p.Name!= "Id");
            // Create a dictionary of the object's properties and their values
            Dictionary<string, object> payload = new Dictionary<string, object>();
            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                if (!payload.ContainsKey(property.Name))
                {
                    payload.Add(property.Name, property.GetValue(obj));
                }
            }
            
            //return Encode(payload, SECRET_KEY, JwtHashAlgorithm.HS256);
            return Encode(payload, JwtHashAlgorithm.HS256);
        }

        public JWT Encode(Dictionary<string, object> load, JwtHashAlgorithm algorithm)
        {
            var claims = new Claim[load.Count];
            int i = 0;
            foreach (var kvp in load)
            {
                claims[i++] = new Claim(kvp.Key, kvp.Value.ToString());
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(GenerateRandomKey()), algorithm.ToString())
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = new JWT { Token = tokenHandler.WriteToken(token) };
            var holder = Decoder<User>(jwt.Token);
            return jwt;
        }

        private static byte[] GenerateRandomKey(int keySize = 256)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var key = new byte[keySize / 8];
                rng.GetBytes(key);
                return key;
            }
        }

        //public JWT Encode<TKey, TValue>(Dictionary<TKey, TValue> load, string Secret, JwtHashAlgorithm algorithm)
        //{
        //    List<Claim> claims = new();

        //    foreach (var (key, value) in load)
        //    {
        //        claims.Add(new Claim(key.ToString(), JsonConvert.SerializeObject(value)));
        //    }
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = DateTime.UtcNow.AddDays(2),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)),
        //        algorithm.ToString())
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return new JWT { Token = tokenHandler.WriteToken(token) };
        //}

        public T Decoder<T>(string jwt) where T : new()
        {
            JwtSecurityToken token = tokenHandler.ReadJwtToken(jwt);
            T obj = new T();

            var claimsDict = token.Claims.ToDictionary(c => c.Type);
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var handlers = new Dictionary<Type, Action<T, Claim>>()
            {
                { typeof(Guid), (o, c) => o.GetType().GetProperty(c.Type).SetValue(o, Guid.Parse(c.Value)) },
                { typeof(string), (o, c) => o.GetType().GetProperty(c.Type).SetValue(o, c.Value) },
                { typeof(DateTime), (o, c) => o.GetType().GetProperty(c.Type).SetValue(o, DateTime.Parse(c.Value)) }
            };

            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                if (claimsDict.TryGetValue(property.Name, out Claim claim))
                {
                    if (property.PropertyType.IsClass)
                    {
                        if (!handlers.TryGetValue(property.PropertyType, out Action<T, Claim> handler))
                            continue;
                        handler(obj, claim);
                    }
                    else if (!handlers.TryGetValue(property.PropertyType, out Action<T, Claim> handler))
                    {
                        continue;
                        handler(obj, claim);
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