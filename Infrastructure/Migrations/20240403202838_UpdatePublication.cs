using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePublication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Publication_PublicationId",
                schema: "public",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Publication_Users_IssuerId",
                schema: "public",
                table: "Publication");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationTheme_Publication_PublicationsId",
                schema: "public",
                table: "PublicationTheme");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationUser_Publication_FavoritesId",
                schema: "public",
                table: "PublicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Reaction_Publication_PublicationId",
                schema: "public",
                table: "Reaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publication",
                schema: "public",
                table: "Publication");

            migrationBuilder.RenameTable(
                name: "Publication",
                schema: "public",
                newName: "Publications",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_Publication_IssuerId",
                schema: "public",
                table: "Publications",
                newName: "IX_Publications_IssuerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publications",
                schema: "public",
                table: "Publications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Publications_PublicationId",
                schema: "public",
                table: "Comments",
                column: "PublicationId",
                principalSchema: "public",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationTheme_Publications_PublicationsId",
                schema: "public",
                table: "PublicationTheme",
                column: "PublicationsId",
                principalSchema: "public",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationUser_Publications_FavoritesId",
                schema: "public",
                table: "PublicationUser",
                column: "FavoritesId",
                principalSchema: "public",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Users_IssuerId",
                schema: "public",
                table: "Publications",
                column: "IssuerId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reaction_Publications_PublicationId",
                schema: "public",
                table: "Reaction",
                column: "PublicationId",
                principalSchema: "public",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Publications_PublicationId",
                schema: "public",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationTheme_Publications_PublicationsId",
                schema: "public",
                table: "PublicationTheme");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationUser_Publications_FavoritesId",
                schema: "public",
                table: "PublicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Users_IssuerId",
                schema: "public",
                table: "Publications");

            migrationBuilder.DropForeignKey(
                name: "FK_Reaction_Publications_PublicationId",
                schema: "public",
                table: "Reaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publications",
                schema: "public",
                table: "Publications");

            migrationBuilder.RenameTable(
                name: "Publications",
                schema: "public",
                newName: "Publication",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_Publications_IssuerId",
                schema: "public",
                table: "Publication",
                newName: "IX_Publication_IssuerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publication",
                schema: "public",
                table: "Publication",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Publication_PublicationId",
                schema: "public",
                table: "Comments",
                column: "PublicationId",
                principalSchema: "public",
                principalTable: "Publication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publication_Users_IssuerId",
                schema: "public",
                table: "Publication",
                column: "IssuerId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationTheme_Publication_PublicationsId",
                schema: "public",
                table: "PublicationTheme",
                column: "PublicationsId",
                principalSchema: "public",
                principalTable: "Publication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationUser_Publication_FavoritesId",
                schema: "public",
                table: "PublicationUser",
                column: "FavoritesId",
                principalSchema: "public",
                principalTable: "Publication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reaction_Publication_PublicationId",
                schema: "public",
                table: "Reaction",
                column: "PublicationId",
                principalSchema: "public",
                principalTable: "Publication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
