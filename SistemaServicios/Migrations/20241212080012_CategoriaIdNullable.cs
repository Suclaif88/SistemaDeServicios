using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaServicios.Migrations
{
    /// <inheritdoc />
    public partial class CategoriaIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_CategoriasServicio_CategoriaId",
                table: "Servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesServicio_Clientes_ClienteId",
                table: "SolicitudesServicio");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesServicio_Servicios_ServicioId",
                table: "SolicitudesServicio");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesServicio_Tecnicos_TecnicoId",
                table: "SolicitudesServicio");

            migrationBuilder.AlterColumn<int>(
                name: "TecnicoId",
                table: "SolicitudesServicio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ServicioId",
                table: "SolicitudesServicio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Comentarios",
                table: "SolicitudesServicio",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "SolicitudesServicio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Servicios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_CategoriasServicio_CategoriaId",
                table: "Servicios",
                column: "CategoriaId",
                principalTable: "CategoriasServicio",
                principalColumn: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesServicio_Clientes_ClienteId",
                table: "SolicitudesServicio",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesServicio_Servicios_ServicioId",
                table: "SolicitudesServicio",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesServicio_Tecnicos_TecnicoId",
                table: "SolicitudesServicio",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "TecnicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_CategoriasServicio_CategoriaId",
                table: "Servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesServicio_Clientes_ClienteId",
                table: "SolicitudesServicio");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesServicio_Servicios_ServicioId",
                table: "SolicitudesServicio");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesServicio_Tecnicos_TecnicoId",
                table: "SolicitudesServicio");

            migrationBuilder.AlterColumn<int>(
                name: "TecnicoId",
                table: "SolicitudesServicio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServicioId",
                table: "SolicitudesServicio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comentarios",
                table: "SolicitudesServicio",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "SolicitudesServicio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Servicios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_CategoriasServicio_CategoriaId",
                table: "Servicios",
                column: "CategoriaId",
                principalTable: "CategoriasServicio",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesServicio_Clientes_ClienteId",
                table: "SolicitudesServicio",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesServicio_Servicios_ServicioId",
                table: "SolicitudesServicio",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "ServicioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesServicio_Tecnicos_TecnicoId",
                table: "SolicitudesServicio",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "TecnicoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
