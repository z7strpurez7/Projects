using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace movieCharacterAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPrimaryKeys1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Francise_FranchiseId",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Francise",
                table: "Francise");

            migrationBuilder.RenameTable(
                name: "Francise",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Francises_FranchiseId",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Francises",
                table: "Francises");

            migrationBuilder.RenameTable(
                name: "Francises",
                newName: "Francise");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Francise",
                table: "Francise",
                column: "FranchiseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Francise_FranchiseId",
                table: "Movies",
                column: "FranchiseId",
                principalTable: "Francise",
                principalColumn: "FranchiseId");
        }
    }
}
