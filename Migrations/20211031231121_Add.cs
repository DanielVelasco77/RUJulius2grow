using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RUJulius2grow.Migrations
{
    public partial class Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Usuario",
               columns: table => new
               {
                   IdUsuario = table.Column<int>(type: "INTEGER", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                   Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                   Password = table.Column<string>(type: "nvarchar(256)", maxLength: 50, nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_IdUsuario", x => x.IdUsuario);
               });          

            migrationBuilder.CreateTable(
               name: "Noticias",
               columns: table => new
               {
                   IdNoticias = table.Column<int>(type: "INTEGER", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   IdUsuario = table.Column<int>(type: "INTEGER", nullable: false),
                   Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                   Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                   Imagen = table.Column<string>(type: "nvarchar(MAX)" , nullable: false),
                   FechaRegistro = table.Column<DateTime>(type: "datetime" ,nullable:false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_IdNoticias", x => x.IdNoticias);
                   table.ForeignKey(
                       name: "FK_Noticias_Usuario_usuarioId",
                       column: x => x.IdUsuario,  
                       principalTable: "Usuario",
                       principalColumn: "IdUsuario",
                       onDelete: ReferentialAction.Restrict);
                       
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "Usuario");
            migrationBuilder.DropTable(
                name: "Noticias");

        }


    }
}
