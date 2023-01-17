using DiscordClone.Api.Entities.Base;

namespace DiscordClone.Api.Entities
{
    public class SecurityQuestion : BaseEntity<int>
    {
        public int LookupId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public PreviousRegisteredEmailLookup Lookup { get; set; }

    }
}