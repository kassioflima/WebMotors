using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMotors.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "teste_webmotors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "varchar(45)", unicode: false, nullable: true),
                    Modelo = table.Column<string>(type: "varchar(45)", unicode: false, nullable: true),
                    Versao = table.Column<string>(type: "varchar(45)", unicode: false, nullable: true),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Quilometragem = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "varchar(512)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teste_webmotors", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "teste_webmotors");
        }
    }
}
