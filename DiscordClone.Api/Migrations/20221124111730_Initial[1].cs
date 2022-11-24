﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscordClone.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bytes = table.Column<byte[]>(type: "varbinary(4000)", maxLength: 4000, nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasePermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
                    Action = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePermission", x => x.Id);
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
                name: "GroupChats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
                    Bytes = table.Column<byte[]>(type: "varbinary(4000)", maxLength: 4000, nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountImageId = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastLogonDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountImages_AccountImageId",
                        column: x => x.AccountImageId,
                        principalTable: "AccountImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChannelPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelPermissions_BasePermission_Id",
                        column: x => x.Id,
                        principalTable: "BasePermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerPermissions_BasePermission_Id",
                        column: x => x.Id,
                        principalTable: "BasePermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountChat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountChat_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountChat_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
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
                name: "GroupChatAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    GroupChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupChatId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChatAccount_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupChatAccount_GroupChats_GroupChatId",
                        column: x => x.GroupChatId,
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupChatAccount_GroupChats_GroupChatId1",
                        column: x => x.GroupChatId1,
                        principalTable: "GroupChats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Message<Chat, Guid>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EditedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message<Chat, Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message<Chat, Guid>_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message<GroupChat, Guid>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message<GroupChat, Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message<GroupChat, Guid>_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message<GroupChat, Guid>_GroupChats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreviousRegisteredEmailLookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PasswordSetDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Message<Chat, Guid>_Id",
                        column: x => x.Id,
                        principalTable: "Message<Chat, Guid>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageAttachment<Chat, Guid>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FileLocation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAttachment<Chat, Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageAttachment<Chat, Guid>_Message<Chat, Guid>_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message<Chat, Guid>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageAttachment<GroupChat, Guid>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FileLocation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAttachment<GroupChat, Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageAttachment<GroupChat, Guid>_Message<GroupChat, Guid>_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message<GroupChat, Guid>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessageAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessageAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessageAttachments_MessageAttachment<Chat, Guid>_Id",
                        column: x => x.Id,
                        principalTable: "MessageAttachment<Chat, Guid>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupChatMessageAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatMessageAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChatMessageAttachments_MessageAttachment<GroupChat, Guid>_Id",
                        column: x => x.Id,
                        principalTable: "MessageAttachment<GroupChat, Guid>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextChannels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoiceChannels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRole",
                columns: table => new
                {
                    AccountsId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRole", x => new { x.AccountsId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AccountRole_Accounts_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleGeneralChannelPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGeneralChannelPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleGeneralChannelPermissions_BasePermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "BasePermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleGeneralChannelPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleGeneralServerPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGeneralServerPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleGeneralServerPermissions_BasePermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "BasePermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleGeneralServerPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleMembershipPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMembershipPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleMembershipPermissions_BasePermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "BasePermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleMembershipPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message<TextChannel, int>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message<TextChannel, int>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message<TextChannel, int>_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message<TextChannel, int>_TextChannels_ChatId",
                        column: x => x.ChatId,
                        principalTable: "TextChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleTextChannelPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTextChannelPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleTextChannelPermissions_BasePermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "BasePermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleTextChannelPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleTextChannelPermissions_TextChannels_EntityId",
                        column: x => x.EntityId,
                        principalTable: "TextChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TextChannelSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
                    TextChannelId = table.Column<int>(type: "int", nullable: true),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextChannelSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextChannelSettings_TextChannels_TextChannelId",
                        column: x => x.TextChannelId,
                        principalTable: "TextChannels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleVoiceChannelPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleVoiceChannelPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleVoiceChannelPermissions_BasePermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "BasePermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleVoiceChannelPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleVoiceChannelPermissions_VoiceChannels_EntityId",
                        column: x => x.EntityId,
                        principalTable: "VoiceChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServerProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ServerId = table.Column<int>(type: "int", nullable: false),
                    ProfileImageId = table.Column<int>(type: "int", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AccountId1 = table.Column<int>(type: "int", nullable: true),
                    VoiceChannelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerProfiles_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerProfiles_Accounts_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerProfiles_ProfileImages_ProfileImageId",
                        column: x => x.ProfileImageId,
                        principalTable: "ProfileImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerProfiles_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerProfiles_VoiceChannels_VoiceChannelId",
                        column: x => x.VoiceChannelId,
                        principalTable: "VoiceChannels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VoiceChannelSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoiceChannelId = table.Column<int>(type: "int", nullable: true),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoiceChannelSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoiceChannelSettings_VoiceChannels_VoiceChannelId",
                        column: x => x.VoiceChannelId,
                        principalTable: "VoiceChannels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessageAttachment<TextChannel, int>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 4"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FileLocation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAttachment<TextChannel, int>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageAttachment<TextChannel, int>_Message<TextChannel, int>_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message<TextChannel, int>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerProfileTextChannel",
                columns: table => new
                {
                    ServerProfileId = table.Column<int>(type: "int", nullable: false),
                    TextChannelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerProfileTextChannel", x => new { x.ServerProfileId, x.TextChannelId });
                    table.ForeignKey(
                        name: "FK_ServerProfileTextChannel_ServerProfiles_ServerProfileId",
                        column: x => x.ServerProfileId,
                        principalTable: "ServerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerProfileTextChannel_TextChannels_TextChannelId",
                        column: x => x.TextChannelId,
                        principalTable: "TextChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TextChannelMessageAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextChannelMessageAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextChannelMessageAttachments_MessageAttachment<TextChannel, int>_Id",
                        column: x => x.Id,
                        principalTable: "MessageAttachment<TextChannel, int>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountChat_AccountId",
                table: "AccountChat",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountChat_ChatId",
                table: "AccountChat",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_RoleId",
                table: "AccountRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountImageId",
                table: "Accounts",
                column: "AccountImageId",
                unique: true);

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
                name: "IX_GroupChatAccount_AccountId",
                table: "GroupChatAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatAccount_GroupChatId",
                table: "GroupChatAccount",
                column: "GroupChatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatAccount_GroupChatId1",
                table: "GroupChatAccount",
                column: "GroupChatId1");

            migrationBuilder.CreateIndex(
                name: "IX_Message<Chat, Guid>_AccountId",
                table: "Message<Chat, Guid>",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Message<GroupChat, Guid>_AccountId",
                table: "Message<GroupChat, Guid>",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Message<GroupChat, Guid>_ChatId",
                table: "Message<GroupChat, Guid>",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Message<TextChannel, int>_AccountId",
                table: "Message<TextChannel, int>",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Message<TextChannel, int>_ChatId",
                table: "Message<TextChannel, int>",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageAttachment<Chat, Guid>_MessageId",
                table: "MessageAttachment<Chat, Guid>",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageAttachment<GroupChat, Guid>_MessageId",
                table: "MessageAttachment<GroupChat, Guid>",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageAttachment<TextChannel, int>_MessageId",
                table: "MessageAttachment<TextChannel, int>",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_PreviousRegisteredEmailLookups_AccountId",
                table: "PreviousRegisteredEmailLookups",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleGeneralChannelPermissions_PermissionId",
                table: "RoleGeneralChannelPermissions",
                column: "PermissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleGeneralChannelPermissions_RoleId",
                table: "RoleGeneralChannelPermissions",
                column: "RoleId");

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecurityQuestions_LookupId",
                table: "SecurityQuestions",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerProfiles_AccountId",
                table: "ServerProfiles",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerProfiles_AccountId1",
                table: "ServerProfiles",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_ServerProfiles_ProfileImageId",
                table: "ServerProfiles",
                column: "ProfileImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServerProfiles_ServerId",
                table: "ServerProfiles",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerProfiles_VoiceChannelId",
                table: "ServerProfiles",
                column: "VoiceChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerProfileTextChannel_TextChannelId",
                table: "ServerProfileTextChannel",
                column: "TextChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_UserId",
                table: "Servers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TextChannels_ServerId",
                table: "TextChannels",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_TextChannelSettings_TextChannelId",
                table: "TextChannelSettings",
                column: "TextChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountId",
                table: "Users",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoiceChannels_ServerId",
                table: "VoiceChannels",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_VoiceChannelSettings_VoiceChannelId",
                table: "VoiceChannelSettings",
                column: "VoiceChannelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountChat");

            migrationBuilder.DropTable(
                name: "AccountRole");

            migrationBuilder.DropTable(
                name: "ChannelPermissions");

            migrationBuilder.DropTable(
                name: "ChatMessageAttachments");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "GroupChatAccount");

            migrationBuilder.DropTable(
                name: "GroupChatMessageAttachments");

            migrationBuilder.DropTable(
                name: "RoleGeneralChannelPermissions");

            migrationBuilder.DropTable(
                name: "RoleGeneralServerPermissions");

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
                name: "ServerProfileTextChannel");

            migrationBuilder.DropTable(
                name: "TextChannelMessageAttachments");

            migrationBuilder.DropTable(
                name: "TextChannelSettings");

            migrationBuilder.DropTable(
                name: "VoiceChannelSettings");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "MessageAttachment<Chat, Guid>");

            migrationBuilder.DropTable(
                name: "MessageAttachment<GroupChat, Guid>");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "PreviousRegisteredEmailLookups");

            migrationBuilder.DropTable(
                name: "BasePermission");

            migrationBuilder.DropTable(
                name: "ServerProfiles");

            migrationBuilder.DropTable(
                name: "MessageAttachment<TextChannel, int>");

            migrationBuilder.DropTable(
                name: "Message<Chat, Guid>");

            migrationBuilder.DropTable(
                name: "Message<GroupChat, Guid>");

            migrationBuilder.DropTable(
                name: "ProfileImages");

            migrationBuilder.DropTable(
                name: "VoiceChannels");

            migrationBuilder.DropTable(
                name: "Message<TextChannel, int>");

            migrationBuilder.DropTable(
                name: "GroupChats");

            migrationBuilder.DropTable(
                name: "TextChannels");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountImages");
        }
    }
}
