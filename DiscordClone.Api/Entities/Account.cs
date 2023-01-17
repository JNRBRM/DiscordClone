using System.Text.Json.Serialization;
using DiscordClone.Api.Entities.Base;
using DiscordClone.Api.Entities.ServerRelated;

namespace DiscordClone.Api.Entities
{
    public class Account : BaseEntity<int>
    {
        public Guid? UserId { get; set; }
        public int ImageId { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLogonDate { get; set; }
        public List<Friend> Friends { get; set; }
   
        public List<AccountChat> AccountChats { get; set; }
     
        public List<AccountGroupChat> AccountGroupChats { get; set; }
        
        public List<ServerProfile> ServerProfiles { get; set; }
        public AccountImage Image { get; set; }
        public User User { get; set; }

    }
}
