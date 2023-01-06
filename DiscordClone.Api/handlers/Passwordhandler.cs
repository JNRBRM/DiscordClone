using DiscordClone.Api.Entities;
using DiscordClone.Api.Interface;
using System.Security.Cryptography;
using System.Text;

namespace DiscordClone.Api.handlers
{
    public class Passwordhandler
    {
        private readonly IPasswordService _PasswordService;
        public async Task<bool> checkPassword(string Password, Guid UserId)
        {
            var UserPassword = await _PasswordService.FindByCondition(obj => obj.UserId == UserId);

            byte[] password = Encoding.ASCII.GetBytes(Password);
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] plainTextWithSaltBytes = new byte[password.Length + UserPassword.Salt.Length];
            for (int i = 0; i < password.Length; i++)
            {
                plainTextWithSaltBytes[i] = password[i];
            }
            for (int i = 0; i < UserPassword.Salt.Length; i++)
            {
                plainTextWithSaltBytes[password.Length + i] = UserPassword.Salt[i];
            }

            if (UserPassword.PasswordHash == plainTextWithSaltBytes)
            {
                return true;
            }
            return false;
        }

        public async Task CreatePassword(string password,Guid Userid)
        {

            // Generate a random salt value.
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Use the PBKDF2 algorithm to generate a hash and salt for the password.
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);
                SecurityCredentials credentials = new()
                {
                    PasswordHash = hash,
                    Salt = salt,
                    UserId = Userid
                };
                await _PasswordService.Create(credentials);
            }
            
        }

        public async Task UpdatePassword(string password, Guid Userid)
        {

            // Generate a random salt value.
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Use the PBKDF2 algorithm to generate a hash and salt for the password.
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);
                SecurityCredentials credentials = new()
                {
                    Id = _PasswordService.FindByCondition(obj => obj.UserId == Userid).Result.Id,
                    PasswordHash = hash,
                    Salt = salt,
                    UserId = Userid
                };
                await _PasswordService.Update(credentials);
            }

        }
        //public async Task<bool> CreatePassword(string Password,Guid UserId)
        //{
        //    var bytes = new byte[16];
        //    using (var rng = new RNGCryptoServiceProvider())
        //    {
        //        rng.GetBytes(bytes);
        //    }
        //    string hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();

        //    byte[] salt = Convert.FromBase64String(hash);

        //    byte[] password = Encoding.ASCII.GetBytes(Password);
        //    HashAlgorithm algorithm = new SHA256Managed();
        //    byte[] plainTextWithSaltBytes = new byte[password.Length + salt.Length];
        //    for (int i = 0; i < password.Length; i++)
        //    {
        //        plainTextWithSaltBytes[i] = password[i];
        //    }
        //    for (int i = 0; i < salt.Length; i++)
        //    {
        //        plainTextWithSaltBytes[password.Length + i] = salt[i];
        //    }
        //    SecurityCredentials SecurityCredentials = new()
        //    {
        //        Salt = salt,
        //        PasswordHash = algorithm.ComputeHash(plainTextWithSaltBytes),
        //        UserId= UserId
        //    };
        //    if (await _PasswordService.Create(SecurityCredentials) is not null)
        //    {
        //        return true;
        //    }
        //    return false;

        //}
    }
}
