namespace DiscordClone.Api.Entities.Base
{
    public abstract class BaseChannelSetting<TChannel> : BaseEntity<int>, IBaseChannelSetting
    where TChannel : IBaseChannel
    {
        public int ChannelId { get; set; }
        public string Name { get; set; }
        public string Parameter { get; set; }
        public string Description { get; set; }

        public TChannel Channel { get; set; }
    }

    public interface IBaseChannelSetting
    {
    }
}