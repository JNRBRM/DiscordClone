using DiscordClone.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;

namespace DiscordClone.Api.DBContext
{
    public class DiscordCloneContext :DbContext
    {
        public DiscordCloneContext(DbContextOptions<DiscordCloneContext> options) : base(options) { }

        #region DbSets
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountImage> AccountImages { get; set; }
        public DbSet<ChannelPermission> ChannelPermissions { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<GroupChatMessage> GroupChatMessages { get; set; }
        //public DbSet<Message> Messages { get; set; }
        public DbSet<GroupChatMessageAttachment> GroupChatMessageAttachments { get; set; }
        public DbSet<ChatMessageAttachment> ChatMessageAttachments { get; set; }
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
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .ConfigureWarnings(wa => wa.Ignore(RelationalEventId.ForeignKeyPropertiesMappedToUnrelatedTables));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Ignore Entity
            modelBuilder.Ignore<BaseChannel>();
            //modelBuilder.Ignore<BasePermission>();
            #endregion

            #region Add Entity
            modelBuilder.Entity<Account>(model =>
            {
                model.HasKey(x => x.Id);

                model.Property(x=>x.DisplayName).HasMaxLength(32).IsRequired();
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.Property(x => x.LastLogonDate).HasDefaultValue(null);

                model.HasMany(x=>x.Chats).WithOne(x=>x.Account).HasForeignKey(x=>x.AccountId);
                model.HasMany(x=>x.ServerProfiles).WithOne(x=>x.Account).HasForeignKey(x=>x.AccountId);
                model.HasMany(x => x.Friends).WithOne(x => x.Account1).HasForeignKey(x => x.AccountId1);

                model.HasOne(x => x.User).WithOne(x => x.Account).HasForeignKey<Account>(x => x.UserId);
                model.HasOne(x => x.Image).WithOne().HasForeignKey<Account>(x => x.AccountImageId);

            });
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<AccountImage>(model =>
            {
                //model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.HasKey(x => x.Id);
                model.Property(x=>x.Bytes).HasMaxLength(4000).IsRequired();
                model.Property(x=>x.FileExtension).HasMaxLength(4).IsRequired();
                model.Property(x=>x.Size).IsRequired();
            });
            modelBuilder.Entity<AccountImage>().ToTable("AccountImages");
            modelBuilder.Entity<ChannelPermission>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                //model.HasKey(x => x.Id);
                model.Property(x => x.Action).HasMaxLength(64).IsRequired();
                model.Property(x => x.Description).HasMaxLength(96).IsRequired();
            });
            modelBuilder.Entity<ChannelPermission>().ToTable("ChannelPermissions");
            modelBuilder.Entity<Chat>(model =>
            {
                // model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.HasKey(x => x.Id);
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.HasMany(x => x.Messages).WithOne(x => x.Chat).HasForeignKey(x => x.ChatId);
                model.HasMany(x => x.Accounts).WithOne(x => x.Chat).HasForeignKey(x => x.ChatId);
            });
            modelBuilder.Entity<Chat>().ToTable("Chats");
            modelBuilder.Entity<Friend>()
                .HasOne(x => x.Account1)
                .WithMany()
                .HasForeignKey(x => x.AccountId1)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Friend>()
                .HasOne(x => x.Account2)
                .WithMany()
                .HasForeignKey(x => x.AccountId2);

            modelBuilder.Entity<Friend>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.Property(x => x.Status).HasConversion<int>().IsRequired();
                model.Property(x => x.SentDate).HasDefaultValueSql("getdate()").IsRequired();
            });
            modelBuilder.Entity<Friend>().ToTable("Friends");
            modelBuilder.Entity<GroupChat>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.HasMany(x => x.Messages).WithOne(x => x.Chat).HasForeignKey(x => x.ChatId);
                model.HasMany(x => x.Accounts).WithOne().HasForeignKey(x => x.GroupChatId);
                model.HasOne(x => x.Owner).WithOne(x=>x.GroupChat).HasForeignKey<GroupChatAccount>(x=>x.GroupChatId);
            });
            modelBuilder.Entity<GroupChat>().ToTable("GroupChats");

            modelBuilder.Entity<ChatMessage>(model =>
            {
               // model.Property(x => x.Id).UseIdentityColumn(1, 4);
                model.HasKey(x => x.Id);
                model.Property(x => x.Content).HasMaxLength(2000).IsRequired();
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.Property(x => x.EditedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.HasMany(x => x.MessageAttachments).WithOne(x => (ChatMessage)x.Message).HasForeignKey(x => x.MessageId);
                model.HasOne(x => x.Chat).WithMany(x => x.Messages).HasForeignKey(x => x.ChatId);
                //model.HasOne(x => x.Account).WithMany(x => x.Messages).HasForeignKey(x => x.AccountId);

                /*model.Property(x => x.Chat.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.HasMany(x => x.Chat.Messages).WithOne(); 
                model.HasMany(x => x.Chat.Accounts).WithOne(); */
            });
            modelBuilder.Entity<ChatMessage>().ToTable("ChatMessages");

            modelBuilder.Entity<GroupChatMessageAttachment>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1, 4);
                model.Property(x => x.Type).HasConversion<int>().IsRequired();
                model.Property(x => x.FileLocation).HasMaxLength(256).IsRequired();
                model.HasOne(x => x.Message).WithMany(x => x.MessageAttachments).HasForeignKey(x => x.MessageId);
            });
            modelBuilder.Entity<GroupChatMessageAttachment>().ToTable("GroupChatMessageAttachments");
            modelBuilder.Entity<ChatMessageAttachment>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.Property(x => x.Type).HasConversion<int>().IsRequired();
                model.Property(x => x.FileLocation).HasMaxLength(256).IsRequired();
                model.HasOne(x => x.Message).WithMany(x => (IEnumerable<ChatMessageAttachment>)x.MessageAttachments).HasForeignKey(x => x.MessageId);
            });
            modelBuilder.Entity<ChatMessageAttachment>().ToTable("ChatMessageAttachments");

            modelBuilder.Entity<TextChannelMessageAttachment>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.Property(x => x.Type).HasConversion<int>().IsRequired();
                model.Property(x => x.FileLocation).HasMaxLength(256).IsRequired();
                model.HasOne(x => x.Message).WithMany(x => (IEnumerable<TextChannelMessageAttachment>)x.MessageAttachments).HasForeignKey(x => x.MessageId);
            });
            modelBuilder.Entity<TextChannelMessageAttachment>().ToTable("TextChannelMessageAttachments");

            //modelBuilder.Entity<MessageAttachment>(model =>
            //{
            //    model.Property(x => x.Id).UseIdentityColumn(1,4);
            //    model.Property(x => x.Type).HasConversion<int>().IsRequired();
            //    model.Property(x => x.FileLocation).HasMaxLength(256).IsRequired();
            //    model.HasOne(x => x.Message).WithMany(x => x.MessageAttachments).HasForeignKey(x => x.MessageId);
            //});
            //modelBuilder.Entity<MessageAttachment>().ToTable("MessageAttachments");

            modelBuilder.Entity<PreviousRegisteredEmailLookup>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.Property(x => x.EmailHash).HasMaxLength(256).IsRequired();
                model.Property(x => x.Salt).HasMaxLength(100).IsRequired();
                model.HasMany(x => x.Questions).WithOne(x => x.Lookup).HasForeignKey(x => x.LookupId);
                model.HasOne(x => x.Account).WithOne().HasForeignKey<PreviousRegisteredEmailLookup>(x => x.AccountId);
            });
            modelBuilder.Entity<PreviousRegisteredEmailLookup>().ToTable("PreviousRegisteredEmailLookups");
            modelBuilder.Entity<ProfileImage>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.Property(x => x.Bytes).HasMaxLength(4000).IsRequired();
                model.Property(x => x.FileExtension).HasMaxLength(4).IsRequired();
                model.Property(x => x.Size).IsRequired();
            });
            modelBuilder.Entity<ProfileImage>().ToTable("ProfileImages");

            modelBuilder.Entity<Role>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.Property(x=>x.Name).HasMaxLength(64).IsRequired();
                model.Property(x => x.Description).HasMaxLength(100).IsRequired();
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.Property(x=>x.IsServerAdmin).HasDefaultValue(false).IsRequired();
                //lave måske en mellem table fx RoleServerProfile
                model.HasMany(x => x.Accounts).WithMany();
                model.HasMany(x => x.RoleGeneralServerPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
                model.HasMany(x => x.RoleGeneralChannelPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
                model.HasMany(x => x.RoleMembershipPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
                model.HasMany(x => x.RoleTextChannelPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
                model.HasMany(x => x.RoleVoiceChannelPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
                model.HasOne(x=>x.Server).WithMany(x=>x.Roles).HasForeignKey(x=>x.ServerId);
            });
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<RoleGeneralChannelPermission>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.HasOne(x => x.Role).WithMany(x=>x.RoleGeneralChannelPermission).HasForeignKey(x => x.RoleId);
                model.HasOne(x => x.Permission).WithOne().HasForeignKey<RoleGeneralChannelPermission>(x => x.PermissionId);

                //model.HasMany(x => x.Entity.Accounts);
                //model.HasOne(x => x.Entity.Server).WithOne().HasForeignKey<RoleGeneralChannelPermission>(x=>x.Entity.ServerId);

                //model.Property(x => x.Entity.Name).HasMaxLength(100).IsRequired();
                //model.Property(x => x.Entity.IsAgeRestricted).HasDefaultValue(false).IsRequired();
            });
            modelBuilder.Entity<RoleGeneralChannelPermission>().ToTable("RoleGeneralChannelPermissions");
            modelBuilder.Entity<RoleGeneralServerPermission>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.HasOne(x => x.Role).WithMany(x => x.RoleGeneralServerPermission).HasForeignKey(x => x.RoleId);
                model.HasOne(x => x.Permission).WithOne().HasForeignKey<RoleGeneralServerPermission>(x => x.PermissionId);
            });
            modelBuilder.Entity<RoleGeneralServerPermission>().ToTable("RoleGeneralServerPermissions");
            modelBuilder.Entity<RoleMembershipPermission>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.HasOne(x => x.Role).WithMany(x => x.RoleMembershipPermission).HasForeignKey(x => x.RoleId);
                model.HasOne(x => x.Permission).WithOne().HasForeignKey<RoleMembershipPermission>(x => x.PermissionId);
            });
            modelBuilder.Entity<RoleMembershipPermission>().ToTable("RoleMembershipPermissions");
            modelBuilder.Entity<RoleTextChannelPermission>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.HasOne(x => x.Role).WithMany(x => x.RoleTextChannelPermission).HasForeignKey(x => x.RoleId);
                model.HasOne(x => x.Permission).WithOne().HasForeignKey<RoleTextChannelPermission>(x => x.PermissionId);

                //model.HasMany(x => x.Entity.Accounts);
                //model.HasOne(x => x.Entity.Server).WithOne().HasForeignKey<RoleGeneralChannelPermission>(x => x.Entity.ServerId);

                //model.Property(x => x.Entity.Name).HasMaxLength(100).IsRequired();
                //model.Property(x => x.Entity.IsAgeRestricted).HasDefaultValue(false).IsRequired();
            });
            modelBuilder.Entity<RoleTextChannelPermission>().ToTable("RoleTextChannelPermissions");
            modelBuilder.Entity<RoleVoiceChannelPermission>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.HasOne(x => x.Role).WithMany(x => x.RoleVoiceChannelPermission).HasForeignKey(x => x.RoleId);
                model.HasOne(x => x.Permission).WithOne().HasForeignKey<RoleVoiceChannelPermission>(x => x.PermissionId);

                //model.HasMany(x => x.Entity.Accounts);
                //model.HasOne(x => x.Entity.Server).WithOne().HasForeignKey<RoleVoiceChannelPermission>(x => x.Entity.ServerId);

                //model.Property(x => x.Entity.Name).HasMaxLength(100).IsRequired();
                //model.Property(x => x.Entity.IsAgeRestricted).HasDefaultValue(false).IsRequired();
            });
            modelBuilder.Entity<RoleVoiceChannelPermission>().ToTable("RoleVoiceChannelPermissions");
            modelBuilder.Entity<SecurityCredentials>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x=>x.PasswordHash).HasMaxLength(256).IsRequired();
                model.Property(x=>x.Salt).HasMaxLength(256).IsRequired();
                model.HasOne(x => x.User).WithOne().HasForeignKey<SecurityCredentials>(x => x.UserId);
            });
            modelBuilder.Entity<SecurityCredentials>().ToTable("SecurityCredentials");
            modelBuilder.Entity<SecurityQuestion>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.Property(x=>x.Question).HasMaxLength(100).IsRequired();
                model.Property(x=>x.Answer).HasMaxLength(100).IsRequired();
                model.HasOne(x => x.Lookup).WithMany(x => x.Questions).HasForeignKey(x => x.LookupId);
            });
            modelBuilder.Entity<SecurityQuestion>().ToTable("SecurityQuestions");

            modelBuilder.Entity<Server>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.Property(x=>x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.HasMany(x => x.ServerProfiles).WithOne(x=>x.Server).HasForeignKey(x => x.ServerId);
                model.HasMany(x => x.Roles).WithOne(x => x.Server).HasForeignKey(x => x.ServerId);
                model.HasMany(x => x.TextChannels).WithOne(x=>x.Server).HasForeignKey(x=>x.ServerId);
                model.HasMany(x => x.VoiceChannels).WithOne(x => x.Server).HasForeignKey(x => x.ServerId);
                model.HasOne(x => x.User).WithOne().HasForeignKey<Server>(x => x.UserId);

            });
            modelBuilder.Entity<Server>().ToTable("Servers");

            modelBuilder.Entity<ServerPermission>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.Property(x => x.Action).HasMaxLength(64).IsRequired();
                model.Property(x => x.Description).HasMaxLength(96).IsRequired();
            });
            modelBuilder.Entity<ServerPermission>().ToTable("ServerPermissions");
            modelBuilder.Entity<ServerProfile>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.Property(x=>x.NickName).HasMaxLength(32).IsRequired();
                model.HasOne(x => x.Image).WithOne().HasForeignKey<ServerProfile>(x => x.ProfileImageId);
                model.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.AccountId);
                model.HasOne(x => x.Server).WithMany(x=>x.ServerProfiles).HasForeignKey(x => x.ServerId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<ServerProfile>().ToTable("ServerProfiles");
            modelBuilder.Entity<TextChannel>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);

                model.HasMany(x => x.ServerProfile).WithMany();
                model.HasOne(x => x.Server).WithMany(x=>x.TextChannels).HasForeignKey(x => x.ServerId);
                model.Property(x => x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.IsAgeRestricted).HasDefaultValue(false).IsRequired();
                model.HasMany(x => x.TextChannelSettings).WithOne();
                model.HasMany(x => x.Messages).WithOne(x=>x.Chat).HasForeignKey(x=>x.ChatId);

            });
            modelBuilder.Entity<TextChannel>().ToTable("TextChannels");
            modelBuilder.Entity<TextChannelSetting>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.Property(x => x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.Parameter).HasMaxLength(100).IsRequired();
                model.Property(x => x.Description).HasMaxLength(100).IsRequired();
                //model.HasOne(x => x.Channel).WithMany().HasForeignKey(x=>x.ChannelId);

             });
            modelBuilder.Entity<TextChannelSetting>().ToTable("TextChannelSettings");
            modelBuilder.Entity<User>(model =>
            {
                //model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.Property(x=>x.Email).HasMaxLength(100).IsRequired();
                model.Property(x=>x.PhoneNumber).HasMaxLength(10).IsRequired();
                model.Property(x=>x.EmailConfirmed).HasDefaultValue(false).IsRequired();
                model.Property(x=>x.PhoneNumberConfirmed).HasDefaultValue(false).IsRequired();
                model.Property(x=>x.PasswordSetDate).HasDefaultValueSql("getdate()").IsRequired();
                model.HasOne(x => x.Account).WithOne(x => x.User).HasForeignKey<User>(x => x.AccountId);
            });
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<VoiceChannel>(model =>
            {
                model.Property(x => x.Id).UseIdentityColumn(1,4);

                //model.HasMany(x => x.ServerProfile).WithMany();
                model.HasOne(x => x.Server).WithMany(x=>x.VoiceChannels).HasForeignKey(x => x.ServerId);
                model.Property(x => x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.IsAgeRestricted).HasDefaultValue(false).IsRequired();
                model.HasMany(x => x.VoiceChannelSettings).WithOne();
            });
            modelBuilder.Entity<VoiceChannel>().ToTable("VoiceChannels");
            modelBuilder.Entity<VoiceChannelSetting>(model =>
            {
                //model.Property(x => x.Id).UseIdentityColumn(1,4);
                model.Property(x => x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.Parameter).HasMaxLength(100).IsRequired();
                model.Property(x => x.Description).HasMaxLength(100).IsRequired();
               // model.HasOne(x => x.Channel).WithMany().HasForeignKey(x => x.ChannelId);

            });
            modelBuilder.Entity<VoiceChannelSetting>().ToTable("VoiceChannelSettings");
            #endregion
        }
    }
}
// Add-migration Initial[V]
// Update-Database