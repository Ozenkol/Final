using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Values_Fields_ValueId",
                table: "Values");

            migrationBuilder.AlterColumn<int>(
                name: "ValueId",
                table: "Values",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Values_FieldId",
                table: "Values",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Fields_FieldId",
                table: "Values",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "FieldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Values_Fields_FieldId",
                table: "Values");

            migrationBuilder.DropIndex(
                name: "IX_Values_FieldId",
                table: "Values");

            migrationBuilder.AlterColumn<int>(
                name: "ValueId",
                table: "Values",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Fields_ValueId",
                table: "Values",
                column: "ValueId",
                principalTable: "Fields",
                principalColumn: "FieldId");
        }
    }
}
