using DiscordClone.Api.Entities.ServerRelated;

namespace DiscordClone.Api.Entities.Base
{
    public abstract class BaseChannel<TGeneralPermission> : BaseEntity<int>, IBaseChannel
    {
        public int ServerId { get; set; }
        public string Name { get; set; }

        public bool IsAgeRestricted { get; set; }
        //public List<TGeneralPermission> GeneralPermissions { get; set; }
        public Server Server { get; set; }
    }

    public interface IBaseChannel
    {
    }
}