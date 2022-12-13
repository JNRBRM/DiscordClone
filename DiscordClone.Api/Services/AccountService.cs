using DiscordClone.Api.Entities;
using DiscordClone.Api.Interface;

namespace DiscordClone.Api.Services
{
    public class AccountService : GenericService<Account, int>, IAccountService
    {
        private readonly IAccountRepository _AccountRepository;
        public AccountService(IAccountRepository AccountRepository) : base(AccountRepository) => _AccountRepository = AccountRepository;
    }
}
