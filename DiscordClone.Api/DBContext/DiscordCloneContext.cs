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
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region BaseSetup
            modelBuilder.Entity<BaseEntity<int>>(model =>
            {
                model.HasKey(x => x.Id);
            });
            modelBuilder.Entity<BaseEntity<Guid>>(model =>
            {
                model.HasKey(x=>x.Id);
            });
            modelBuilder.Entity<BaseChannel>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.IsAgeRestricted).HasDefaultValue(false).IsRequired();

                model.HasMany(x => x.GeneralPermissions).WithOne(x=>x.Entity).HasForeignKey(x=>x.EntityId);
            });
            modelBuilder.Entity<BaseChannel>().UseTpcMappingStrategy();
            modelBuilder.Entity<BaseChannelSetting>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.Parameter).HasMaxLength(100).IsRequired();
                model.Property(x => x.Description).HasMaxLength(100).IsRequired();
            });
            modelBuilder.Entity<BaseChannelSetting>().UseTpcMappingStrategy();
            modelBuilder.Entity<BaseChat<GroupChatMessage, AccountGroupChat>>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.HasMany(x => x.Messages).WithOne(x=>x.Chat).HasForeignKey(x=>x.ChatId);
                model.HasMany(x => x.Accounts).WithOne(x=>x.GroupChat).HasForeignKey(x=>x.ChatId);
            });
            modelBuilder.Entity<BaseChat<GroupChatMessage, AccountGroupChat>>().UseTpcMappingStrategy();
            modelBuilder.Entity<BaseChat<ChatMessage, AccountChat>>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.HasMany(x => x.Messages).WithOne(x => x.Chat).HasForeignKey(x => x.ChatId);
                model.HasMany(x => x.Accounts).WithOne(x => x.Chat).HasForeignKey(x => x.ChatId);
            });
            modelBuilder.Entity<BaseChat<ChatMessage, AccountChat>>().UseTpcMappingStrategy();
            modelBuilder.Entity<BaseImage>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.Bytes).HasMaxLength(4000).IsRequired();
                model.Property(x => x.FileExtension).HasMaxLength(4).IsRequired();
                model.Property(x => x.Size).IsRequired();
            });
            modelBuilder.Entity<BaseImage>().UseTpcMappingStrategy();
            modelBuilder.Entity<BaseMessage<Chat, Guid>>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.Content).HasMaxLength(2000).IsRequired();
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.Property(x => x.EditedDate).HasDefaultValueSql("getdate()").IsRequired();
            });
            modelBuilder.Entity<BaseMessage<Chat, Guid>>().UseTpcMappingStrategy();
            modelBuilder.Entity<BasePermission>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.Action).HasMaxLength(64).IsRequired();
                model.Property(x => x.Description).HasMaxLength(96).IsRequired();
            });
            modelBuilder.Entity<BasePermission>().UseTpcMappingStrategy();
            modelBuilder.Entity<BaseRolePermission>(model =>
            {
                model.HasKey(x => x.Id);
                model.HasOne(x => x.Permission).WithMany().HasForeignKey(x=>x.PermissionId).OnDelete(DeleteBehavior.Cascade);
                model.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<BaseRolePermission>().UseTpcMappingStrategy();
            modelBuilder.Entity<BaseMessageAttachment<ChatMessage,Guid>>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.FileLocation).HasMaxLength(4000).IsRequired(true);
                model.Property(x => x.Type).HasConversion<int>().IsRequired(true);
                model.HasOne(x => x.Message).WithMany(x=>x.MessageAttachments).HasForeignKey(x => x.MessageId).OnDelete(DeleteBehavior.Cascade);
                
            });
            modelBuilder.Entity<BaseMessageAttachment<ChatMessage,Guid>>().UseTpcMappingStrategy();
            modelBuilder.Entity<BaseMessageAttachment<GroupChatMessage, Guid>>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.FileLocation).HasMaxLength(4000).IsRequired(true);
                model.Property(x => x.Type).HasConversion<int>().IsRequired(true);
                model.HasOne(x => x.Message).WithMany(x => x.MessageAttachments).HasForeignKey(x => x.MessageId).OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<BaseMessageAttachment<GroupChatMessage, Guid>>().UseTpcMappingStrategy();
            modelBuilder.Entity<BaseMessageAttachment<TextChannelMessage, int>>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.FileLocation).HasMaxLength(4000).IsRequired(true);
                model.Property(x => x.Type).HasConversion<int>().IsRequired(true);
                model.HasOne(x => x.Message).WithMany(x => x.MessageAttachments).HasForeignKey(x => x.MessageId).OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<BaseMessageAttachment<TextChannelMessage, int>>().UseTpcMappingStrategy();
            #endregion

            #region Add Entity
            modelBuilder.Entity<Account>(model =>
            {
                model.Property(x => x.DisplayName).HasMaxLength(32).IsRequired(true);
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired(true);
                model.Property(x => x.LastLogonDate).IsRequired(false);
                model.HasMany(x => x.AccountChats).WithOne(x => x.Account).HasForeignKey(x => x.AccountId).IsRequired(true);
                model.HasMany(x => x.AccountGroupChats).WithOne(x => x.Account).HasForeignKey(x => x.AccountId).IsRequired(true);
                model.HasMany(x => x.ServerProfiles).WithOne(x => x.Account).HasForeignKey(x => x.AccountId).IsRequired(true);
                model.HasOne(x => x.Image).WithOne().HasForeignKey<Account>(x=>x.ImageId);
            });
            modelBuilder.Entity<Account>().UseTpcMappingStrategy().ToTable("Accounts");
            modelBuilder.Entity<AccountGroupChat>(model =>
            {
            });
            modelBuilder.Entity<AccountImage>().UseTpcMappingStrategy().ToTable("AccountImages");
            modelBuilder.Entity<ChannelPermission>().UseTpcMappingStrategy().ToTable("ChannelPermissions");
            modelBuilder.Entity<Chat>().UseTpcMappingStrategy().ToTable("Chats");
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
                model.HasKey(x => x.Id);
                model.Property(x => x.Status).HasConversion<int>().IsRequired();
                model.Property(x => x.SentDate).HasDefaultValueSql("getdate()").IsRequired();
            });
            modelBuilder.Entity<Friend>().UseTpcMappingStrategy().ToTable("Friends");

            modelBuilder.Entity<GroupChat>(model =>
            {
                model.Property(x=>x.Name).HasMaxLength(100);
                model.HasOne(x => x.Owner).WithOne(x => x.GroupChat).HasForeignKey<GroupChat>(x => x.OwnerId);
            });
            modelBuilder.Entity<GroupChat>().UseTpcMappingStrategy().ToTable("GroupChats");
            
            modelBuilder.Entity<ChatMessage>(model =>
            {
              
            });
            modelBuilder.Entity<ChatMessage>().UseTpcMappingStrategy().ToTable("ChatMessages");
            
            modelBuilder.Entity<BaseMessageAttachment<GroupChat,Guid>>(model =>
            {
              
            });
            modelBuilder.Entity<BaseMessageAttachment<GroupChat, Guid>>().UseTpcMappingStrategy();
            modelBuilder.Entity<GroupChatMessageAttachment>().UseTpcMappingStrategy().ToTable("GroupChatMessageAttachments");
            modelBuilder.Entity<ChatMessageAttachment>(model =>
            {
                
            });
            modelBuilder.Entity<ChatMessageAttachment>().ToTable("ChatMessageAttachments");

            modelBuilder.Entity<BaseMessageAttachment<TextChannel, int>>().UseTpcMappingStrategy();
            modelBuilder.Entity<BaseMessageAttachment<TextChannel, int>>(model =>
            {
                
            });
            modelBuilder.Entity<TextChannelMessageAttachment>().ToTable("TextChannelMessageAttachments");

           

            modelBuilder.Entity<PreviousRegisteredEmailLookup>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.EmailHash).HasMaxLength(256).IsRequired();
                model.Property(x => x.Salt).HasMaxLength(100).IsRequired();
                //model.HasMany(x => x.Questions).WithOne(x => x.Lookup).HasForeignKey(x => x.LookupId);
                //model.HasOne(x => x.Account).WithOne().HasForeignKey<PreviousRegisteredEmailLookup>(x => x.AccountId);
            });
            modelBuilder.Entity<PreviousRegisteredEmailLookup>().UseTpcMappingStrategy().ToTable("PreviousRegisteredEmailLookups");
           
            
            modelBuilder.Entity<ProfileImage>().UseTpcMappingStrategy().ToTable("ProfileImages");

            modelBuilder.Entity<Role>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x=>x.Name).HasMaxLength(64).IsRequired();
                model.Property(x => x.Description).HasMaxLength(100).IsRequired();
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                model.Property(x=>x.IsServerAdmin).HasDefaultValue(false).IsRequired();
                //lave måske en mellem table fx RoleServerProfile
                //model.HasMany(x => x.Accounts).WithMany();
                //model.HasMany(x => x.RoleGeneralServerPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
                //model.HasMany(x => x.RoleGeneralChannelPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
                //model.HasMany(x => x.RoleMembershipPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
                //model.HasMany(x => x.RoleTextChannelPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
                //model.HasMany(x => x.RoleVoiceChannelPermission).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
                //model.HasOne(x=>x.Server).WithMany(x=>x.Roles).HasForeignKey(x=>x.ServerId);
            });
            modelBuilder.Entity<Role>().UseTpcMappingStrategy().ToTable("Roles");
            modelBuilder.Entity<RoleGeneralChannelPermission>(model =>
            {
                model.HasKey(x => x.Id);
                model.HasOne(x => x.Role).WithMany(x=>x.RoleGeneralChannelPermission).HasForeignKey(x => x.RoleId);
                model.HasOne(x => x.Permission).WithOne().HasForeignKey<RoleGeneralChannelPermission>(x => x.PermissionId);

                //model.HasMany(x => x.Entity.Accounts);
                //model.HasOne(x => x.Entity.Server).WithOne().HasForeignKey<RoleGeneralChannelPermission>(x=>x.Entity.ServerId);

                //model.Property(x => x.Entity.Name).HasMaxLength(100).IsRequired();
                //model.Property(x => x.Entity.IsAgeRestricted).HasDefaultValue(false).IsRequired();
            });
            modelBuilder.Entity<RoleGeneralChannelPermission>().UseTpcMappingStrategy().ToTable("RoleGeneralChannelPermissions");
            modelBuilder.Entity<RoleGeneralServerPermission>(model =>
            {
                model.HasKey(x => x.Id);
                //model.HasOne(x => x.Role).WithMany(x => x.RoleGeneralServerPermission).HasForeignKey(x => x.RoleId);
                //model.HasOne(x => x.Permission).WithOne().HasForeignKey<RoleGeneralServerPermission>(x => x.PermissionId);
            });
            modelBuilder.Entity<RoleGeneralServerPermission>().UseTpcMappingStrategy().ToTable("RoleGeneralServerPermissions");
            modelBuilder.Entity<RoleMembershipPermission>(model =>
            {
                model.HasKey(x => x.Id);
                //model.HasOne(x => x.Role).WithMany(x => x.RoleMembershipPermission).HasForeignKey(x => x.RoleId);
                //model.HasOne(x => x.Permission).WithOne().HasForeignKey<RoleMembershipPermission>(x => x.PermissionId);
            });
            modelBuilder.Entity<RoleMembershipPermission>().UseTpcMappingStrategy().ToTable("RoleMembershipPermissions");
            modelBuilder.Entity<RoleTextChannelPermission>(model =>
            {
                model.HasKey(x => x.Id);
                //model.HasOne(x => x.Role).WithMany(x => x.RoleTextChannelPermission).HasForeignKey(x => x.RoleId);
                //model.HasOne(x => x.Permission).WithOne().HasForeignKey<RoleTextChannelPermission>(x => x.PermissionId);

                //model.HasMany(x => x.Entity.Accounts);
                //model.HasOne(x => x.Entity.Server).WithOne().HasForeignKey<RoleGeneralChannelPermission>(x => x.Entity.ServerId);

                //model.Property(x => x.Entity.Name).HasMaxLength(100).IsRequired();
                //model.Property(x => x.Entity.IsAgeRestricted).HasDefaultValue(false).IsRequired();
            });
            modelBuilder.Entity<RoleTextChannelPermission>().UseTpcMappingStrategy().ToTable("RoleTextChannelPermissions");
            modelBuilder.Entity<RoleVoiceChannelPermission>(model =>
            {
                model.HasKey(x => x.Id);
                //model.HasOne(x => x.Role).WithMany(x => x.RoleVoiceChannelPermission).HasForeignKey(x => x.RoleId);
                //model.HasOne(x => x.Permission).WithOne().HasForeignKey<RoleVoiceChannelPermission>(x => x.PermissionId);

                //model.HasMany(x => x.Entity.Accounts);
                //model.HasOne(x => x.Entity.Server).WithOne().HasForeignKey<RoleVoiceChannelPermission>(x => x.Entity.ServerId);

                //model.Property(x => x.Entity.Name).HasMaxLength(100).IsRequired();
                //model.Property(x => x.Entity.IsAgeRestricted).HasDefaultValue(false).IsRequired();
            });
            modelBuilder.Entity<RoleVoiceChannelPermission>().UseTpcMappingStrategy().ToTable("RoleVoiceChannelPermissions");
            modelBuilder.Entity<SecurityCredentials>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x=>x.PasswordHash).HasMaxLength(256).IsRequired();
                model.Property(x=>x.Salt).HasMaxLength(256).IsRequired();
                //model.HasOne(x => x.User).WithOne().HasForeignKey<SecurityCredentials>(x => x.UserId);
            });
            modelBuilder.Entity<SecurityCredentials>().UseTpcMappingStrategy().ToTable("SecurityCredentials");
            modelBuilder.Entity<SecurityQuestion>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x=>x.Question).HasMaxLength(100).IsRequired();
                model.Property(x=>x.Answer).HasMaxLength(100).IsRequired();
                //model.HasOne(x => x.Lookup).WithMany(x => x.Questions).HasForeignKey(x => x.LookupId);
            });
            modelBuilder.Entity<SecurityQuestion>().UseTpcMappingStrategy().ToTable("SecurityQuestions");

            modelBuilder.Entity<Server>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x=>x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
                //model.HasMany(x => x.ServerProfiles).WithOne(x=>x.Server).HasForeignKey(x => x.ServerId);
                //model.HasMany(x => x.Roles).WithOne(x => x.Server).HasForeignKey(x => x.ServerId);
                //model.HasMany(x => x.TextChannels).WithOne(x=>x.Server).HasForeignKey(x=>x.ServerId);
                //model.HasMany(x => x.VoiceChannels).WithOne(x => x.Server).HasForeignKey(x => x.ServerId);
                //model.HasOne(x => x.User).WithOne().HasForeignKey<Server>(x => x.UserId);

            });
            modelBuilder.Entity<Server>().UseTpcMappingStrategy().ToTable("Servers");

            
            modelBuilder.Entity<ServerPermission>().ToTable("ServerPermissions");
            modelBuilder.Entity<ServerProfile>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x=>x.NickName).HasMaxLength(32).IsRequired();
                //model.HasOne(x => x.Image).WithOne().HasForeignKey<ServerProfile>(x => x.ProfileImageId);
                //model.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.AccountId);
                //model.HasOne(x => x.Server).WithMany(x=>x.ServerProfiles).HasForeignKey(x => x.ServerId)
                //.OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<ServerProfile>().UseTpcMappingStrategy().ToTable("ServerProfiles");
            
            modelBuilder.Entity<TextChannel>(model =>
            {
                model.HasMany(x => x.Permissions).WithOne(x => x.Entity).HasForeignKey(x => x.EntityId).OnDelete(DeleteBehavior.Cascade);
                model.HasOne(x => x.Server).WithMany(x => x.TextChannels).HasForeignKey(x => x.ServerId).OnDelete(DeleteBehavior.Cascade);
                //model.HasMany(x => x.ServerProfile).WithMany();
                //model.HasOne(x => x.Server).WithMany(x=>x.TextChannels).HasForeignKey(x => x.ServerId);
                model.HasMany(x => x.TextChannelSettings).WithOne().HasForeignKey(x=>x.ChannelId).OnDelete(DeleteBehavior.Cascade);
                model.HasMany(x => x.Messages).WithOne(x=>x.Chat).HasForeignKey(x=>x.ChatId).OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<TextChannel>().UseTpcMappingStrategy().ToTable("TextChannels");
            modelBuilder.Entity<TextChannelSetting>(model =>
            {
                model.HasKey(x => x.Id);
                

             });
            modelBuilder.Entity<TextChannelSetting>().UseTpcMappingStrategy().ToTable("TextChannelSettings");
            modelBuilder.Entity<User>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x=>x.Email).HasMaxLength(100).IsRequired();
                model.Property(x=>x.PhoneNumber).HasMaxLength(10).IsRequired();
                model.Property(x=>x.EmailConfirmed).HasDefaultValue(false).IsRequired();
                model.Property(x=>x.PhoneNumberConfirmed).HasDefaultValue(false).IsRequired();
                model.Property(x=>x.PasswordSetDate).HasDefaultValueSql("getdate()").IsRequired();
                //model.HasOne(x => x.Account).WithOne(x => x.User).HasForeignKey<User>(x => x.AccountId);
            });
            modelBuilder.Entity<User>().UseTpcMappingStrategy().ToTable("Users");
            modelBuilder.Entity<VoiceChannel>(model =>
            {
                ////model.HasKey(x => x.Id);

                //model.HasMany(x => x.ServerProfile).WithMany();
                //model.HasOne(x => x.Server).WithMany(x=>x.VoiceChannels).HasForeignKey(x => x.ServerId);
                ////model.Property(x => x.Name).HasMaxLength(100).IsRequired();
                ////model.Property(x => x.IsAgeRestricted).HasDefaultValue(false).IsRequired();
                //model.HasMany(x => x.VoiceChannelSettings).WithOne();
            });
            modelBuilder.Entity<VoiceChannel>().UseTpcMappingStrategy().ToTable("VoiceChannels");
            modelBuilder.Entity<VoiceChannelSetting>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.Name).HasMaxLength(100).IsRequired();
                model.Property(x => x.Parameter).HasMaxLength(100).IsRequired();
                model.Property(x => x.Description).HasMaxLength(100).IsRequired();
               // model.HasOne(x => x.Channel).WithMany().HasForeignKey(x => x.ChannelId);

            }); 
            modelBuilder.Entity<VoiceChannelSetting>().UseTpcMappingStrategy().ToTable("VoiceChannelSettings");
            #endregion
        }
    }
}
// Add-migration Initial[V]
// Update-Database