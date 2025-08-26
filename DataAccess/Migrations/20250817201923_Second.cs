using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Inventories_FieldId",
                table: "Fields");

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

            migrationBuilder.AlterColumn<int>(
                name: "FieldId",
                table: "Fields",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Fields_InventoryId",
                table: "Fields",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Inventories_InventoryId",
                table: "Fields",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Fields_ValueId",
                table: "Values",
                column: "ValueId",
                principalTable: "Fields",
                principalColumn: "FieldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Inventories_InventoryId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Values_Fields_ValueId",
                table: "Values");

            migrationBuilder.DropIndex(
                name: "IX_Fields_InventoryId",
                table: "Fields");

            migrationBuilder.AlterColumn<int>(
                name: "ValueId",
                table: "Values",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "FieldId",
                table: "Fields",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Values_FieldId",
                table: "Values",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Inventories_FieldId",
                table: "Fields",
                column: "FieldId",
                principalTable: "Inventories",
                principalColumn: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Fields_FieldId",
                table: "Values",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "FieldId");
        }
    }
}
