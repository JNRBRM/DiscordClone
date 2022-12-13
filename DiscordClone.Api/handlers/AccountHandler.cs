using DiscordClone.Api.Entities;
using DiscordClone.Api.Interface;
using DiscordClone.Api.Services;

namespace DiscordClone.Api.handlers
{
    public class AccountHandler
    {
        private readonly AccountService _AccountService;

        public async Task<int> CreateAccount(Account Account)
        {
            await _AccountService.Create(Account);
            //var AccountId = await _AccountService.FindByCondition(obj => obj.);
            return 2;
        }
    }
}
