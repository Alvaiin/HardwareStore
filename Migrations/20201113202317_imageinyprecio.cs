using Microsoft.EntityFrameworkCore.Migrations;

namespace hardStore.Migrations
{
    public partial class imageinyprecio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Producto",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Precio",
                table: "Producto",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Producto");
        }
    }
}
