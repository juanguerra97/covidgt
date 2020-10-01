using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CovidGtAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CasosCovid",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Localizacion_Pais = table.Column<string>(maxLength: 128, nullable: true),
                    Localizacion_Departamento = table.Column<string>(maxLength: 32, nullable: true),
                    Localizacion_Municipio = table.Column<string>(maxLength: 128, nullable: true),
                    FechaRegistro = table.Column<DateTime>(nullable: false),
                    Edad = table.Column<int>(nullable: false),
                    Sexo = table.Column<string>(fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasosCovid", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CasosCovid_Localizacion_Departamento",
                table: "CasosCovid",
                column: "Localizacion_Departamento");

            migrationBuilder.CreateIndex(
                name: "IX_CasosCovid_Localizacion_Municipio",
                table: "CasosCovid",
                column: "Localizacion_Municipio");

            migrationBuilder.CreateIndex(
                name: "IX_CasosCovid_Localizacion_Pais",
                table: "CasosCovid",
                column: "Localizacion_Pais");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CasosCovid");
        }
    }
}
