using System;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:public.reaction_type", "like,dislike")
                .Annotation("Npgsql:Enum:public.role", "default,moderator,admin")
                .Annotation("Npgsql:Enum:public.status", "denied,moderation,published");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                schema: "public",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Role>(
                name: "Role",
                schema: "public",
                table: "Users",
                type: "role",
                nullable: false,
                defaultValue: Role.Default);

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publication",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<Status>(type: "status", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    IssuerId = table.Column<int>(type: "integer", nullable: true),
                    XmlDocumentUrl = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publication_Users_IssuerId",
                        column: x => x.IssuerId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Themes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "public",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Posted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    IssuerId = table.Column<int>(type: "integer", nullable: false),
                    PublicationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Publication_PublicationId",
                        column: x => x.PublicationId,
                        principalSchema: "public",
                        principalTable: "Publication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_IssuerId",
                        column: x => x.IssuerId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PublicationUser",
                schema: "public",
                columns: table => new
                {
                    FavoritesId = table.Column<int>(type: "integer", nullable: false),
                    FavouritesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationUser", x => new { x.FavoritesId, x.FavouritesId });
                    table.ForeignKey(
                        name: "FK_PublicationUser_Publication_FavoritesId",
                        column: x => x.FavoritesId,
                        principalSchema: "public",
                        principalTable: "Publication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationUser_Users_FavouritesId",
                        column: x => x.FavouritesId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicationTheme",
                schema: "public",
                columns: table => new
                {
                    PublicationsId = table.Column<int>(type: "integer", nullable: false),
                    ThemesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationTheme", x => new { x.PublicationsId, x.ThemesId });
                    table.ForeignKey(
                        name: "FK_PublicationTheme_Publication_PublicationsId",
                        column: x => x.PublicationsId,
                        principalSchema: "public",
                        principalTable: "Publication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationTheme_Themes_ThemesId",
                        column: x => x.ThemesId,
                        principalSchema: "public",
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reaction",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReactionType = table.Column<ReactionType>(type: "reaction_type", nullable: false),
                    IssuerId = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    CommentId = table.Column<int>(type: "integer", nullable: true),
                    PublicationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reaction_Comments_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "public",
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reaction_Publication_PublicationId",
                        column: x => x.PublicationId,
                        principalSchema: "public",
                        principalTable: "Publication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reaction_Users_IssuerId",
                        column: x => x.IssuerId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IssuerId",
                schema: "public",
                table: "Comments",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PublicationId",
                schema: "public",
                table: "Comments",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_IssuerId",
                schema: "public",
                table: "Publication",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationTheme_ThemesId",
                schema: "public",
                table: "PublicationTheme",
                column: "ThemesId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationUser_FavouritesId",
                schema: "public",
                table: "PublicationUser",
                column: "FavouritesId");

            migrationBuilder.CreateIndex(
                name: "IX_Reaction_CommentId",
                schema: "public",
                table: "Reaction",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reaction_IssuerId",
                schema: "public",
                table: "Reaction",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reaction_PublicationId",
                schema: "public",
                table: "Reaction",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Themes_CategoryId",
                schema: "public",
                table: "Themes",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicationTheme",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PublicationUser",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Reaction",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Themes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Comments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Publication",
                schema: "public");

            migrationBuilder.DropColumn(
                name: "Login",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                schema: "public",
                table: "Users");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:public.reaction_type", "like,dislike")
                .OldAnnotation("Npgsql:Enum:public.role", "default,moderator,admin")
                .OldAnnotation("Npgsql:Enum:public.status", "denied,moderation,published");
        }
    }
}
