using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace movieCharacterAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFranchiseTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Francises_FranchiseId",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Francises",
                table: "Francises");

            migrationBuilder.RenameTable(
                name: "Francises",
                newName: "Franchises");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Franchises",
                table: "Franchises",
                column: "FranchiseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Franchises_FranchiseId",
                table: "Movies",
                column: "FranchiseId",
                principalTable: "Franchises",
                principalColumn: "FranchiseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Franchises_FranchiseId",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Franchises",
                table: "Franchises");

            migrationBuilder.RenameTable(
                name: "Franchises",
                newName: "Francises");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Francises",
                table: "Francises",
                column: "FranchiseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Francises_FranchiseId",
                table: "Movies",
                column: "FranchiseId",
                principalTable: "Francises",
                principalColumn: "FranchiseId");
        }
    }
}
