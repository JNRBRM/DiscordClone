using DiscordClone.Api.Entities.ServerRelated;

namespace DiscordClone.Api.Entities.Base
{
    public abstract class BaseRolePermission : BaseEntity<int>
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public Role Role { get; set; }
        public BasePermission Permission { get; set; }
    }
    public abstract class BaseRolePermission<TEntity> : BaseRolePermission where TEntity : IEntity
    {
        public int EntityId { get; set; }
        public TEntity Entity { get; set; }
    }
}