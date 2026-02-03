using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleBM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAtivoToClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Clientes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Clientes");
        }
    }
}
