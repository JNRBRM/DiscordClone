namespace DiscordClone.Api.DataModels
{
    public class CachedItem<T, IdType>
    {
        public IdType Id { get; set; }
        public T Item { get; set; }
    }
}
