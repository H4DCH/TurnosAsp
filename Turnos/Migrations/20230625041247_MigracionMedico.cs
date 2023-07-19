using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turnos.Migrations
{
    /// <inheritdoc />
    public partial class MigracionMedico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    IdMedico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreMedico = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ApellidoMedico = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DireccionMedico = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    HorarioAtencioDesde = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false),
                    HorarioAtencioHasta = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.IdMedico);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medico");
        }
    }
}
