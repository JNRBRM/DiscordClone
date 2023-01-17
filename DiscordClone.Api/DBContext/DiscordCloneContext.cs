using DiscordClone.Api.Entities;
using DiscordClone.Api.Entities.Base;
using DiscordClone.Api.Entities.ServerRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;

namespace DiscordClone.Api.DBContext
{
    public class DiscordCloneContext : DbContext
    {
        public DiscordCloneContext(DbContextOptions<DiscordCloneContext> options) : base(options) { }

        
        #region DbSets
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountChat> AccountChats { get; set; }
        public DbSet<AccountGroupChat> AccountGroupChats { get; set; }
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
        public DbSet<ServerProfileImage> ProfileImages { get; set; }
        public DbSet<Role> Roles { get; set; }
        //public DbSet<RoleGeneralTextChannelPermission> RoleGeneralTextChannelPermissions { get; set; }
        //public DbSet<RoleGeneralVoiceChannelPermission> RoleGeneralVoiceChannelPermissions { get; set; }
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
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region BaseSetup
            modelBuilder.Entity<BaseEntity<int>>(model =>
            {
                model.HasKey(x => x.Id);
            });
            modelBuilder.Entity<BaseEntity<int>>().UseTpcMappingStrategy();
            modelBuilder.Entity<BaseEntity<Guid>>(model =>
            {
                model.HasKey(x => x.Id);
            });
            modelBuilder.Entity<BaseEntity<Guid>>().UseTpcMappingStrategy();
            modelBuilder.Entity<BaseChannel<RoleGeneralTextChannelPermission>>(model =>
            {
                model.Property(x => x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.IsAgeRestricted).HasDefaultValue(false).IsRequired();
                //model.HasMany(x => x.GeneralPermissions).WithOne(x=>x.Entity).HasForeignKey(x=>x.EntityId);
            });
            modelBuilder.Entity<BaseChannel<RoleGeneralTextChannelPermission>>();
            modelBuilder.Entity<BaseChannel<RoleGeneralVoiceChannelPermission>>(model =>
            {
                model.Property(x => x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.IsAgeRestricted).HasDefaultValue(false).IsRequired();
                //model.HasMany(x => x.GeneralPermissions).WithOne(x => x.Entity).HasForeignKey(x => x.EntityId);
            });
            modelBuilder.Entity<BaseChannel<RoleGeneralVoiceChannelPermission>>();
            modelBuilder.Entity<BaseChannelSetting<VoiceChannel>>(model =>
            {
                model.Property(x => x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.Parameter).HasMaxLength(100).IsRequired();
                model.Property(x => x.Description).HasMaxLength(100).IsRequired();
            });
            modelBuilder.Entity<BaseChannelSetting<VoiceChannel>>();
            modelBuilder.Entity<BaseChannelSetting<TextChannel>>(model =>
            {
                model.Property(x => x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.Parameter).HasMaxLength(100).IsRequired();
                model.Property(x => x.Description).HasMaxLength(100).IsRequired();
            });
            modelBuilder.Entity<BaseChannelSetting<TextChannel>>();
            modelBuilder.Entity<BaseChat<GroupChatMessage, AccountGroupChat>>(model =>
            {
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                //model.HasMany(x => x.Messages).WithOne(x=>(GroupChat)x.Chat).HasForeignKey(x=>x.ChatId);
                //model.HasMany(x => x.Accounts).WithOne(x=>x.GroupChat).HasForeignKey(x=>x.ChatId);
            });
            modelBuilder.Entity<BaseChat<GroupChatMessage, AccountGroupChat>>();
            modelBuilder.Entity<BaseChat<ChatMessage, AccountChat>>(model =>
            {
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                //model.HasMany(x => x.Messages).WithOne(x => (Chat)x.Chat).HasForeignKey(x => x.ChatId);
                //model.HasMany(x => x.Accounts).WithOne(x => x.Chat).HasForeignKey(x => x.ChatId);
            });
            modelBuilder.Entity<BaseChat<ChatMessage, AccountChat>>();
            modelBuilder.Entity<BaseImage>(model =>
            {
                model.Property(x => x.Bytes).HasMaxLength(4000).IsRequired();
                model.Property(x => x.FileExtension).HasMaxLength(4).IsRequired();
                model.Property(x => x.Size).HasColumnType("decimal(8,0)").IsRequired();
            });
            modelBuilder.Entity<BaseImage>();
            modelBuilder.Entity<BaseMessage<Chat, Guid>>(model =>
            {
                model.Property(x => x.Content).HasMaxLength(2000).IsRequired();
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.Property(x => x.EditedDate).HasDefaultValueSql("getdate()").IsRequired();
            });
            modelBuilder.Entity<BaseMessage<Chat, Guid>>();
            modelBuilder.Entity<BaseMessage<GroupChat, Guid>>(model =>
            {
                model.Property(x => x.Content).HasMaxLength(2000).IsRequired();
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.Property(x => x.EditedDate).HasDefaultValueSql("getdate()").IsRequired();
            });
            modelBuilder.Entity<BaseMessage<GroupChat, Guid>>();
            modelBuilder.Entity<BasePermission>(model =>
            {
                model.Property(x => x.Action).HasMaxLength(64).IsRequired();
                model.Property(x => x.Description).HasMaxLength(96).IsRequired();
            });
            modelBuilder.Entity<BasePermission>();
            modelBuilder.Entity<BaseRolePermission>(model =>
            {
                model.HasOne(x => x.Permission).WithMany().HasForeignKey(x => x.PermissionId).OnDelete(DeleteBehavior.NoAction);
                model.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<BaseRolePermission>();
            modelBuilder.Entity<BaseMessageAttachment<Chat, Guid, ChatMessage>>(model =>
            {
                model.Property(x => x.FileLocation).HasMaxLength(4000).IsRequired(true);
                model.Property(x => x.Type).HasConversion<int>().IsRequired(true);
                //model.HasOne(x => x.Message).WithMany(x => x.MessageAttachments).HasForeignKey(x => x.MessageId).OnDelete(DeleteBehavior.NoAction);

            });
            modelBuilder.Entity<BaseMessageAttachment<Chat, Guid, ChatMessage>>();
            modelBuilder.Entity<BaseMessageAttachment<GroupChat, Guid, GroupChatMessage>>(model =>
            {
                model.Property(x => x.FileLocation).HasMaxLength(4000).IsRequired(true);
                model.Property(x => x.Type).HasConversion<int>().IsRequired(true);
                //model.HasOne(x => x.Message).WithMany(x => x.MessageAttachments).HasForeignKey(x => x.MessageId).OnDelete(DeleteBehavior.NoAction);

            });
            modelBuilder.Entity<BaseMessageAttachment<GroupChat, Guid, GroupChatMessage>>();
            modelBuilder.Entity<BaseMessageAttachment<TextChannel, int, TextChannelMessage>>(model =>
            {
                model.Property(x => x.FileLocation).HasMaxLength(4000).IsRequired(true);
                model.Property(x => x.Type).HasConversion<int>().IsRequired(true);
                //model.HasOne(x => x.Message).WithMany(x => x.MessageAttachments).HasForeignKey(x => x.MessageId).OnDelete(DeleteBehavior.NoAction);

            });
            modelBuilder.Entity<BaseMessageAttachment<TextChannel, int, TextChannelMessage>>();
            //modelBuilder.Entity<BaseRoleChannelPermission<BaseChannel>>(model =>
            //{
            //    //model.HasOne(x => x.Role).WithMany(x => x.RoleGeneralChannelPermission).HasForeignKey(x => x.RoleId);
            //    model.HasOne(x => x.Permission).WithMany().HasForeignKey(x => x.PermissionId);
            //});
            #endregion

            #region Add Entity
            modelBuilder.Entity<Account>(model =>
            {
                model.Property(x => x.DisplayName).HasMaxLength(32).IsRequired(true);
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired(true);
                model.Property(x => x.LastLogonDate).IsRequired(false);
                model.HasMany(x => x.ServerProfiles).WithOne(x => x.Account).HasForeignKey(x => x.AccountId).IsRequired(true);
                model.HasOne(x => x.Image).WithOne().HasForeignKey<Account>(x => x.ImageId);
            });
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<AccountChat>(model =>
            {
                model.HasOne(x => x.Account).WithMany(x => x.AccountChats).HasForeignKey(x => x.AccountId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
                model.HasOne(x => x.Chat).WithMany(x => x.ChatRelations).HasForeignKey(x => x.ChatId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<AccountGroupChat>(model =>
            {
                model.HasOne(x => x.Account).WithMany(x => x.AccountGroupChats).HasForeignKey(x => x.AccountId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
                model.HasOne(x => x.GroupChat).WithMany(x => x.ChatRelations).HasForeignKey(x => x.ChatId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<AccountChat>().ToTable("AccountChats");
            modelBuilder.Entity<AccountGroupChat>().ToTable("AccountGroupChats");
            modelBuilder.Entity<AccountImage>().ToTable("AccountImages");
            modelBuilder.Entity<ChannelPermission>().ToTable("ChannelPermissions");
            modelBuilder.Entity<Chat>().ToTable("Chats");
            modelBuilder.Entity<ChatMessage>(model =>
            {
                model.HasMany(x => x.MessageAttachments).WithOne(x => x.Message).HasForeignKey(x => x.MessageId).OnDelete(DeleteBehavior.Cascade);
                model.HasOne(x => x.Chat).WithMany(x => x.Messages).HasForeignKey(x => x.ChatId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<ChatMessage>().ToTable("ChatMessages");
            modelBuilder.Entity<ChatMessageAttachment>().ToTable("ChatMessageAttachments");
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
                model.Property(x => x.Status).HasConversion<int>().IsRequired();
                model.Property(x => x.SentDate).HasDefaultValueSql("getdate()").IsRequired();
            });
            modelBuilder.Entity<Friend>().ToTable("Friends");

            modelBuilder.Entity<GroupChat>(model =>
            {
                model.Property(x => x.Name).HasMaxLength(100);
                model.HasOne(x => x.Owner).WithOne().HasForeignKey<GroupChat>(x => x.OwnerId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });
            modelBuilder.Entity<GroupChat>().ToTable("GroupChats");
            modelBuilder.Entity<GroupChatMessage>(model =>
            {
                model.HasMany(x => x.MessageAttachments).WithOne(x => x.Message).HasForeignKey(x => x.MessageId).OnDelete(DeleteBehavior.NoAction);
                model.HasOne(x => x.Chat).WithMany(x => x.Messages).HasForeignKey(x => x.ChatId).OnDelete(DeleteBehavior.NoAction);

            });
            modelBuilder.Entity<TextChannelMessage>(model =>
            {
                model.HasMany(x => x.MessageAttachments).WithOne(x => x.Message).HasForeignKey(x => x.MessageId).OnDelete(DeleteBehavior.NoAction);
                model.HasOne(x => x.Chat).WithMany(x => x.Messages).HasForeignKey(x => x.ChatId).OnDelete(DeleteBehavior.NoAction);

            });
            modelBuilder.Entity<GroupChatMessage>().ToTable("GroupChatMessages");
            modelBuilder.Entity<GroupChatMessageAttachment>().ToTable("GroupChatMessageAttachments");
            modelBuilder.Entity<PreviousRegisteredEmailLookup>(model =>
            {
                model.Property(x => x.EmailHash).HasMaxLength(256).IsRequired();
                model.Property(x => x.Salt).HasMaxLength(100).IsRequired();
                model.HasMany(x => x.Questions).WithOne(x => x.Lookup).HasForeignKey(x => x.LookupId).IsRequired();
                model.HasOne(x => x.Account).WithOne().HasForeignKey<PreviousRegisteredEmailLookup>(x => x.AccountId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });
            modelBuilder.Entity<PreviousRegisteredEmailLookup>().ToTable("PreviousRegisteredEmailLookups");
            modelBuilder.Entity<Role>(model =>
            {
                model.Property(x => x.Name).HasMaxLength(64).IsRequired();
                model.Property(x => x.Description).HasMaxLength(100).IsRequired();
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.Property(x => x.IsServerAdmin).HasDefaultValue(false).IsRequired();
                //lave måske en mellem table fx RoleServerProfile
                //model.HasMany(x => x.Profiles).WithMany();
                model.HasMany(x => x.RoleGeneralServerPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                //model.HasMany(x => x.RoleGeneralTextChannelPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                model.HasMany(x => x.RoleGeneralVoiceChannelPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                model.HasMany(x => x.RoleMembershipPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                model.HasMany(x => x.RoleTextChannelPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                model.HasMany(x => x.RoleVoiceChannelPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                model.HasOne(x => x.Server).WithMany(x => x.Roles).HasForeignKey(x => x.ServerId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });
            modelBuilder.Entity<Role>().ToTable("Roles");
            //modelBuilder.Entity<RoleGeneralTextChannelPermission>(model =>
            //{
            //    model.HasOne(x => x.Entity).WithMany(x=>x.GeneralPermissions).HasForeignKey(x => x.EntityId).OnDelete(DeleteBehavior.NoAction);
            //    model.HasOne(x => x.Permission).WithOne().HasForeignKey<RoleGeneralTextChannelPermission>(x => x.PermissionId).OnDelete(DeleteBehavior.NoAction);
            //    model.HasOne(x => x.Role).WithMany(e=>e.RoleGeneralTextChannelPermission).HasForeignKey(x => x.PermissionId).OnDelete(DeleteBehavior.NoAction);
            //});
            //modelBuilder.Entity<RoleGeneralTextChannelPermission>().ToTable("RoleGeneralTextChannelPermissions");
            //modelBuilder.Entity<RoleGeneralVoiceChannelPermission>(model =>
            //{
            //    model.HasOne(x => x.Entity).WithMany(x => x.GeneralPermissions).HasForeignKey(x => x.EntityId);
            //    model.HasOne(x => x.Permission).WithOne().HasForeignKey<RoleGeneralVoiceChannelPermission>(x => x.PermissionId);
            //});
            modelBuilder.Entity<RoleGeneralVoiceChannelPermission>().ToTable("RoleGeneralVoiceChannelPermissions");
            modelBuilder.Entity<RoleGeneralServerPermission>().ToTable("RoleGeneralServerPermissions");
            modelBuilder.Entity<RoleMembershipPermission>().ToTable("RoleMembershipPermissions");
            modelBuilder.Entity<RoleTextChannelPermission>(model =>
            {
                model.HasOne(x => x.Entity).WithMany(x => x.Permissions).HasForeignKey(x => x.EntityId);
                model.HasOne(x => x.Permission).WithOne().HasForeignKey<RoleTextChannelPermission>(x => x.PermissionId);
            });
            modelBuilder.Entity<RoleTextChannelPermission>().ToTable("RoleTextChannelPermissions");
            modelBuilder.Entity<RoleVoiceChannelPermission>(model =>
            {
                model.HasOne(x => x.Entity).WithMany(x => x.Permissions).HasForeignKey(x => x.EntityId);
                model.HasOne(x => x.Permission).WithOne().HasForeignKey<RoleVoiceChannelPermission>(x => x.PermissionId);
            });
            modelBuilder.Entity<RoleVoiceChannelPermission>().ToTable("RoleVoiceChannelPermissions");
            modelBuilder.Entity<SecurityCredentials>(model =>
            {
                model.Property(x => x.PasswordHash).HasMaxLength(256).IsRequired();
                model.Property(x => x.Salt).HasMaxLength(256).IsRequired();
                model.HasOne(x => x.User).WithOne().HasForeignKey<SecurityCredentials>(x => x.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });
            modelBuilder.Entity<SecurityCredentials>().ToTable("SecurityCredentials");
            modelBuilder.Entity<SecurityQuestion>(model =>
            {
                model.Property(x => x.Question).HasMaxLength(100).IsRequired();
                model.Property(x => x.Answer).HasMaxLength(100).IsRequired();
                //model.HasOne(x => x.Lookup).WithMany(x => x.Questions).HasForeignKey(x => x.LookupId);
            });
            modelBuilder.Entity<SecurityQuestion>().ToTable("SecurityQuestions");
            modelBuilder.Entity<ServerProfileImage>().ToTable("ProfileImages");

            modelBuilder.Entity<Server>(model =>
            {
                model.Property(x => x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.HasMany(x => x.ServerProfiles).WithOne(x => x.Server).HasForeignKey(x => x.ServerId).OnDelete(DeleteBehavior.Cascade).IsRequired();
                model.HasMany(x => x.Roles).WithOne(x => x.Server).HasForeignKey(x => x.ServerId).OnDelete(DeleteBehavior.Cascade).IsRequired();
                model.HasMany(x => x.TextChannels).WithOne(x => x.Server).HasForeignKey(x => x.ServerId).OnDelete(DeleteBehavior.Cascade).IsRequired();
                model.HasMany(x => x.VoiceChannels).WithOne(x => x.Server).HasForeignKey(x => x.ServerId).OnDelete(DeleteBehavior.Cascade).IsRequired();
                model.HasOne(x => x.User).WithOne().HasForeignKey<Server>(x => x.UserId).OnDelete(DeleteBehavior.Cascade).IsRequired();

            });
            modelBuilder.Entity<Server>().ToTable("Servers");


            modelBuilder.Entity<ServerPermission>().ToTable("ServerPermissions");
            modelBuilder.Entity<ServerProfile>(model =>
            {
                model.Property(x => x.NickName).HasMaxLength(32).IsRequired();
                model.HasOne(x => x.Image).WithOne(x => x.ServerProfile).HasForeignKey<ServerProfileImage>(x => x.ServerProfileId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                model.HasOne(x => x.Account).WithMany(x => x.ServerProfiles).HasForeignKey(x => x.AccountId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                model.HasOne(x => x.Server).WithMany(x => x.ServerProfiles).HasForeignKey(x => x.ServerId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });
            modelBuilder.Entity<ServerProfile>().ToTable("ServerProfiles");
            modelBuilder.Entity<ServerProfileImage>(model =>
            {
                model.HasOne(x => x.ServerProfile).WithOne(x => x.Image).HasForeignKey<ServerProfileImage>(x => x.ServerProfileId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });
            modelBuilder.Entity<ServerProfileImage>().ToTable("ServerProfileImages");
            modelBuilder.Entity<TextChannel>(model =>
            {
                model.HasMany(x => x.TextChannelSettings).WithOne(x => (TextChannel)x.Channel).HasForeignKey(x => x.ChannelId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                model.HasMany(x => x.Messages).WithOne(x => x.Chat).HasForeignKey(x => x.ChatId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                model.HasMany(x => x.Permissions).WithOne(x => x.Entity).HasForeignKey(x => x.EntityId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                model.HasOne(x => x.Server).WithMany(x => x.TextChannels).HasForeignKey(x => x.ServerId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });
            modelBuilder.Entity<TextChannel>().ToTable("TextChannels");
            modelBuilder.Entity<TextChannelMessage>().ToTable("TextChannelMessages");
            modelBuilder.Entity<TextChannelMessageAttachment>().ToTable("TextChannelMessageAttachments");
            modelBuilder.Entity<TextChannelSetting>(model =>
            {
                model.HasOne(x => (TextChannel)x.Channel).WithMany(x => x.TextChannelSettings).HasForeignKey(x => x.ChannelId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });
            modelBuilder.Entity<TextChannelSetting>().ToTable("TextChannelSettings");
            modelBuilder.Entity<User>(model =>
            {
                model.Property(x => x.Email).HasMaxLength(100).IsRequired();
                model.Property(x => x.PhoneNumber).HasMaxLength(10).IsRequired();
                model.Property(x => x.EmailConfirmed).HasDefaultValue(false).IsRequired();
                model.Property(x => x.PhoneNumberConfirmed).HasDefaultValue(false).IsRequired();
                model.Property(x => x.PasswordSetDate).HasDefaultValueSql("getdate()").IsRequired();
                model.HasOne(x => x.Account).WithOne(x => x.User).HasForeignKey<Account>(x => x.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
            });
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<VoiceChannel>(model =>
            {
                model.HasMany(x => x.VoiceChannelSettings).WithOne(x => (VoiceChannel)x.Channel).HasForeignKey(x => x.ChannelId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                model.HasMany(x => x.Permissions).WithOne(x => x.Entity).HasForeignKey(x => x.EntityId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                model.HasOne(x => x.Server).WithMany(x => x.VoiceChannels).HasForeignKey(x => x.ServerId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });
            modelBuilder.Entity<VoiceChannel>().ToTable("VoiceChannels");
            modelBuilder.Entity<VoiceChannelSetting>(model =>
            {
                model.HasOne(x => x.Channel).WithMany(x => x.VoiceChannelSettings).HasForeignKey(x => x.ChannelId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });
            modelBuilder.Entity<VoiceChannelSetting>().ToTable("VoiceChannelSettings");
            #endregion
        }
    }
}
//Add - migration Initial[V]
// Update - Database