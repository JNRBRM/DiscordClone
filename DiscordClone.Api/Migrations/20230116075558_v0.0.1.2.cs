using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscordClone.Api.Migrations
{
    /// <inheritdoc />
    public partial class v0012 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessageAttachments_ChatMessages_MessageId",
                table: "ChatMessageAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Chats_ChatId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupChats_AccountGroupChats_OwnerId",
                table: "GroupChats");

            migrationBuilder.DropTable(
                name: "AccountChat>");

            migrationBuilder.DropTable(
                name: "AccountGroupChat>");

            migrationBuilder.DropTable(
                name: "BaseImage");

            migrationBuilder.DropTable(
                name: "BasePermission");

            migrationBuilder.DropTable(
                name: "BaseRolePermission");

            migrationBuilder.DropTable(
                name: "ChatMessage>");

            migrationBuilder.DropTable(
                name: "GroupChatMessage>");

            migrationBuilder.DropTable(
                name: "Guid>");

            migrationBuilder.DropTable(
                name: "RoleGeneralTextChannelPermission>");

            migrationBuilder.DropTable(
                name: "RoleGeneralVoiceChannelPermission>");

            migrationBuilder.DropTable(
                name: "TextChannel>");

            migrationBuilder.DropTable(
                name: "TextChannelMessage>");

            migrationBuilder.DropTable(
                name: "VoiceChannel>");

            migrationBuilder.DropIndex(
                name: "IX_AccountGroupChats_BaseChatId",
                table: "AccountGroupChats");

            migrationBuilder.DropColumn(
                name: "BaseChatId",
                table: "AccountGroupChats");

            migrationBuilder.CreateIndex(
                name: "IX_AccountGroupChats_ChatId",
                table: "AccountGroupChats",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountGroupChats_GroupChats_ChatId",
                table: "AccountGroupChats",
                column: "ChatId",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessageAttachments_ChatMessages_MessageId",
                table: "ChatMessageAttachments",
                column: "MessageId",
                principalTable: "ChatMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Chats_ChatId",
                table: "ChatMessages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChats_Accounts_OwnerId",
                table: "GroupChats",
                column: "OwnerId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountGroupChats_GroupChats_ChatId",
                table: "AccountGroupChats");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessageAttachments_ChatMessages_MessageId",
                table: "ChatMessageAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Chats_ChatId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupChats_Accounts_OwnerId",
                table: "GroupChats");

            migrationBuilder.DropIndex(
                name: "IX_AccountGroupChats_ChatId",
                table: "AccountGroupChats");

            migrationBuilder.AddColumn<Guid>(
                name: "BaseChatId",
                table: "AccountGroupChats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountChat>",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountChat>", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountGroupChat>",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountGroupChat>", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    Bytes = table.Column<byte[]>(type: "varbinary(4000)", maxLength: 4000, nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Size = table.Column<decimal>(type: "decimal(8,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasePermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    Action = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseRolePermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseRolePermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessage>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    FileLocation = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage>", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupChatMessage>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    FileLocation = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatMessage>", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guid>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EditedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guid>_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleGeneralTextChannelPermission>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    ServerId = table.Column<int>(type: "int", nullable: false),
                    IsAgeRestricted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGeneralTextChannelPermission>", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleGeneralVoiceChannelPermission>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    ServerId = table.Column<int>(type: "int", nullable: false),
                    IsAgeRestricted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGeneralVoiceChannelPermission>", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TextChannel>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextChannel>", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TextChannelMessage>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    FileLocation = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextChannelMessage>", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoiceChannel>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoiceChannel>", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountGroupChats_BaseChatId",
                table: "AccountGroupChats",
                column: "BaseChatId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseRolePermission_PermissionId",
                table: "BaseRolePermission",
                column: "PermissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guid>_AccountId",
                table: "Guid>",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessageAttachments_ChatMessages_MessageId",
                table: "ChatMessageAttachments",
                column: "MessageId",
                principalTable: "ChatMessages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Chats_ChatId",
                table: "ChatMessages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChats_AccountGroupChats_OwnerId",
                table: "GroupChats",
                column: "OwnerId",
                principalTable: "AccountGroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
