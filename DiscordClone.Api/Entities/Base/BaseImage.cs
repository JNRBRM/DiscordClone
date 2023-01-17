namespace DiscordClone.Api.Entities.Base
{
    public abstract class BaseImage : BaseEntity<int>
    {
        public byte[] Bytes { get; set; }
        public string FileExtension { get; set; }
        public decimal Size { get; set; }
    }
}