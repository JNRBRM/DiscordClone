using DiscordClone.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscordClone.Api.DBContext
{
    public class DiscordCloneContext :DbContext
    {
        public DiscordCloneContext(DbContextOptions<DiscordCloneContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountImage> AccountImages { get; set; }
        public DbSet<ChannelPermission> ChannelPermissions { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageAttachment> MessageAttachments { get; set; }
        public DbSet<PreviousRegisteredEmailLookup> PreviousRegisteredEmailLookups { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleGeneralChannelPermission> RoleGeneralChannelPermissions { get; set; }
        public DbSet<RoleGeneralServerPermission> RoleGeneralServerPermissions { get; set; }
        public DbSet<RoleMembershipPermission> RoleMembershipPermissions { get; set; }
        public DbSet<RoleTextChannelPermission> RoleTextChannelPermissions { get; set; }
        public DbSet<RoleVoiceChannelPermission> RoleVoiceChannelPermissions { get; set; }
        public DbSet<SecurityCredentials> SecurityCredentials { get; set; }
        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<ServerPermission> ServerPermissions { get; set; }
        public DbSet<ServerProfile> ServerProfiles { get; set; }
        public DbSet<TextChannel> TextChannels { get; set; }
        public DbSet<TextChannelSetting> TextChannelSettings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VoiceChannel> VoiceChannels { get; set; }
        public DbSet<VoiceChannelSetting> VoiceChannelSettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>(model =>
            {
                model.HasKey(x => x.Id);

                model.Property(x=>x.DisplayName).HasMaxLength(32).IsRequired();
                model.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now).IsRequired();
                model.Property(x => x.LastLogonDate).HasDefaultValue(null);

                model.HasMany(x=>x.Messages).WithOne(x=>x.Account).HasForeignKey(x=>x.AccountId);
                model.HasMany(x=>x.Chats).WithOne(x=>x.Account).HasForeignKey(x=>x.AccountId);
                model.HasMany(x=>x.ServerProfiles).WithOne(x=>x.Account).HasForeignKey(x=>x.AccountId);
                model.HasOne(x=>x.Image).WithOne().HasForeignKey(x=>x.AccountId);
            });
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<AccountImage>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x=>x.Bytes).HasMaxLength(4000).IsRequired();
                model.Property(x=>x.FileExtension).HasMaxLength(4).IsRequired();
                model.Property(x=>x.Size).IsRequired();
            });
            modelBuilder.Entity<AccountImage>().ToTable("AccountImages");
            modelBuilder.Entity<ChannelPermission>(model =>
            {
                model.HasKey(x => x.Id);

                model.Property(x=>x.Action).IsRequired();
                model.Property(x=>x.Description).IsRequired();
            });
            modelBuilder.Entity<ChannelPermission>().ToTable("ChannelPermissions");
        }
    }
}
