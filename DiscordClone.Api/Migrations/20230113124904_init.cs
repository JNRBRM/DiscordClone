using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscordClone.Api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "BaseEntitySequence");

            migrationBuilder.CreateTable(
                name: "AccountImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    Bytes = table.Column<byte[]>(type: "varbinary(4000)", maxLength: 4000, nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Size = table.Column<decimal>(type: "decimal(8,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChannelPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    Action = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    Action = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PasswordSetDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastLogonDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountImages_ImageId",
                        column: x => x.ImageId,
                        principalTable: "AccountImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SecurityCredentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(256)", maxLength: 256, nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityCredentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityCredentials_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountChats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountChats_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountChats_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountGroupChats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseChatGroupChatMessageAccountGroupChatId = table.Column<Guid>(name: "BaseChat<GroupChatMessage, AccountGroupChat>Id", type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountGroupChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountGroupChats_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EditedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    AccountId1 = table.Column<int>(type: "int", nullable: false),
                    AccountId2 = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friends_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Friends_Accounts_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friends_Accounts_AccountId2",
                        column: x => x.AccountId2,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreviousRegisteredEmailLookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    EmailHash = table.Column<byte[]>(type: "varbinary(256)", maxLength: 256, nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreviousRegisteredEmailLookups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreviousRegisteredEmailLookups_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsServerAdmin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ServerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TextChannels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    ServerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsAgeRestricted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextChannels_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VoiceChannels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    ServerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsAgeRestricted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoiceChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoiceChannels_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupChats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChats_AccountGroupChats_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AccountGroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessageAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FileLocation = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessageAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessageAttachments_ChatMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ChatMessages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SecurityQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    LookupId = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityQuestions_PreviousRegisteredEmailLookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "PreviousRegisteredEmailLookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleGeneralServerPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGeneralServerPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleGeneralServerPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleMembershipPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMembershipPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleMembershipPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ServerId = table.Column<int>(type: "int", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerProfiles_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerProfiles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerProfiles_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleTextChannelPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTextChannelPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleTextChannelPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleTextChannelPermissions_TextChannels_EntityId",
                        column: x => x.EntityId,
                        principalTable: "TextChannels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TextChannelMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextChannelMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextChannelMessages_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TextChannelMessages_TextChannels_ChatId",
                        column: x => x.ChatId,
                        principalTable: "TextChannels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TextChannelSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextChannelSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextChannelSettings_TextChannels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "TextChannels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleGeneralVoiceChannelPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGeneralVoiceChannelPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleGeneralVoiceChannelPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleGeneralVoiceChannelPermissions_VoiceChannels_EntityId",
                        column: x => x.EntityId,
                        principalTable: "VoiceChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleVoiceChannelPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleVoiceChannelPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleVoiceChannelPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleVoiceChannelPermissions_VoiceChannels_EntityId",
                        column: x => x.EntityId,
                        principalTable: "VoiceChannels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VoiceChannelSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoiceChannelSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoiceChannelSettings_VoiceChannels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "VoiceChannels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EditedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChatMessages_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupChatMessages_GroupChats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "GroupChats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerProfileImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    Bytes = table.Column<byte[]>(type: "varbinary(4000)", maxLength: 4000, nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Size = table.Column<decimal>(type: "decimal(8,0)", nullable: false),
                    ServerProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerProfileImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerProfileImages_ServerProfiles_ServerProfileId",
                        column: x => x.ServerProfileId,
                        principalTable: "ServerProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TextChannelMessageAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FileLocation = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextChannelMessageAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextChannelMessageAttachments_TextChannelMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "TextChannelMessages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupChatMessageAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FileLocation = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatMessageAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChatMessageAttachments_GroupChatMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "GroupChatMessages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountChats_AccountId",
                table: "AccountChats",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountChats_ChatId",
                table: "AccountChats",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountGroupChats_AccountId",
                table: "AccountGroupChats",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountGroupChats_BaseChat<GroupChatMessage, AccountGroupChat>Id",
                table: "AccountGroupChats",
                column: "BaseChat<GroupChatMessage, AccountGroupChat>Id");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ImageId",
                table: "Accounts",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessageAttachments_MessageId",
                table: "ChatMessageAttachments",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_AccountId",
                table: "ChatMessages",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatId",
                table: "ChatMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_AccountId",
                table: "Friends",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_AccountId1",
                table: "Friends",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_AccountId2",
                table: "Friends",
                column: "AccountId2");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatMessageAttachments_MessageId",
                table: "GroupChatMessageAttachments",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatMessages_AccountId",
                table: "GroupChatMessages",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatMessages_ChatId",
                table: "GroupChatMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChats_OwnerId",
                table: "GroupChats",
                column: "OwnerId",
                unique: true,
                filter: "[OwnerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PreviousRegisteredEmailLookups_AccountId",
                table: "PreviousRegisteredEmailLookups",
                column: "AccountId",
                unique: true,
                filter: "[AccountId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGeneralServerPermissions_PermissionId",
                table: "RoleGeneralServerPermissions",
                column: "PermissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleGeneralServerPermissions_RoleId",
                table: "RoleGeneralServerPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGeneralVoiceChannelPermissions_EntityId",
                table: "RoleGeneralVoiceChannelPermissions",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGeneralVoiceChannelPermissions_PermissionId",
                table: "RoleGeneralVoiceChannelPermissions",
                column: "PermissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleGeneralVoiceChannelPermissions_RoleId",
                table: "RoleGeneralVoiceChannelPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMembershipPermissions_PermissionId",
                table: "RoleMembershipPermissions",
                column: "PermissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleMembershipPermissions_RoleId",
                table: "RoleMembershipPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ServerId",
                table: "Roles",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleTextChannelPermissions_EntityId",
                table: "RoleTextChannelPermissions",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleTextChannelPermissions_PermissionId",
                table: "RoleTextChannelPermissions",
                column: "PermissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleTextChannelPermissions_RoleId",
                table: "RoleTextChannelPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleVoiceChannelPermissions_EntityId",
                table: "RoleVoiceChannelPermissions",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleVoiceChannelPermissions_PermissionId",
                table: "RoleVoiceChannelPermissions",
                column: "PermissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleVoiceChannelPermissions_RoleId",
                table: "RoleVoiceChannelPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityCredentials_UserId",
                table: "SecurityCredentials",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityQuestions_LookupId",
                table: "SecurityQuestions",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerProfileImages_ServerProfileId",
                table: "ServerProfileImages",
                column: "ServerProfileId",
                unique: true,
                filter: "[ServerProfileId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ServerProfiles_AccountId",
                table: "ServerProfiles",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerProfiles_RoleId",
                table: "ServerProfiles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerProfiles_ServerId",
                table: "ServerProfiles",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_UserId",
                table: "Servers",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TextChannelMessageAttachments_MessageId",
                table: "TextChannelMessageAttachments",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_TextChannelMessages_AccountId",
                table: "TextChannelMessages",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TextChannelMessages_ChatId",
                table: "TextChannelMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_TextChannels_ServerId",
                table: "TextChannels",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_TextChannelSettings_ChannelId",
                table: "TextChannelSettings",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_VoiceChannels_ServerId",
                table: "VoiceChannels",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_VoiceChannelSettings_ChannelId",
                table: "VoiceChannelSettings",
                column: "ChannelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountChats");

            migrationBuilder.DropTable(
                name: "ChannelPermissions");

            migrationBuilder.DropTable(
                name: "ChatMessageAttachments");

            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "GroupChatMessageAttachments");

            migrationBuilder.DropTable(
                name: "RoleGeneralServerPermissions");

            migrationBuilder.DropTable(
                name: "RoleGeneralVoiceChannelPermissions");

            migrationBuilder.DropTable(
                name: "RoleMembershipPermissions");

            migrationBuilder.DropTable(
                name: "RoleTextChannelPermissions");

            migrationBuilder.DropTable(
                name: "RoleVoiceChannelPermissions");

            migrationBuilder.DropTable(
                name: "SecurityCredentials");

            migrationBuilder.DropTable(
                name: "SecurityQuestions");

            migrationBuilder.DropTable(
                name: "ServerPermissions");

            migrationBuilder.DropTable(
                name: "ServerProfileImages");

            migrationBuilder.DropTable(
                name: "TextChannelMessageAttachments");

            migrationBuilder.DropTable(
                name: "TextChannelSettings");

            migrationBuilder.DropTable(
                name: "VoiceChannelSettings");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "GroupChatMessages");

            migrationBuilder.DropTable(
                name: "PreviousRegisteredEmailLookups");

            migrationBuilder.DropTable(
                name: "ServerProfiles");

            migrationBuilder.DropTable(
                name: "TextChannelMessages");

            migrationBuilder.DropTable(
                name: "VoiceChannels");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "GroupChats");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TextChannels");

            migrationBuilder.DropTable(
                name: "AccountGroupChats");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountImages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropSequence(
                name: "BaseEntitySequence");
        }
    }
}
