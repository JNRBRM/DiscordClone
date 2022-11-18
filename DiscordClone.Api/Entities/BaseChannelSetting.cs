namespace DiscordClone.Api.Entities
{
    public abstract class BaseChannelSetting : BaseEntity<int>
    {
        public int ChannelId { get; set; }
        public string Name { get; set; }
        public string Parameter { get; set; }
        public string Description { get; set; }

        public BaseChannel Channel { get; set; }
    }
}