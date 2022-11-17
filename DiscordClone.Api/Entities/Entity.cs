namespace DiscordClone.Api.Entities
{
    public abstract class Entity<TId> :Entity, IEntity<TId>
    {
        public new TId Id { get; set; }
    }
    public abstract class Entity : IEntity
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
