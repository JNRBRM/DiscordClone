namespace DiscordClone.Api.Entities
{
    public abstract class BaseEntity<TId> : BaseEntity, IEntity<TId>
    {
        public new TId Id { get; set; }
    }
    public abstract class BaseEntity : IEntity
    {
        public object Id { get; set; }
    }
    public interface IEntity<TId> : IEntity
    {
        public new TId Id { get; set; }
    }
    public interface IEntity
    {
        object Id { get; set; }
    }

}
