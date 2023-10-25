using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestePontual.Migrations
{
    /// <inheritdoc />
    public partial class PopularTabelaClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Clientes(Nome, Numero) VALUES('Jean Carlo', '816')");
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Clientes");
        }
    }
}
