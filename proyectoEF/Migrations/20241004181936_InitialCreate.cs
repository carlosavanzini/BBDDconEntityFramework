using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoEF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Peso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cama = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Tarea",
                columns: table => new
                {
                    TareaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityTask = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarea", x => x.TareaId);
                    table.ForeignKey(
                        name: "FK_Tarea_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Cama", "Description", "Name", "Peso" },
                values: new object[] { new Guid("54210ee2-e34f-4a73-87ba-5be75a47c3b6"), "20", null, "Activades pendientes", null });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Cama", "Description", "Name", "Peso" },
                values: new object[] { new Guid("54210ee2-e34f-4a73-87ba-5be75a47c3c4"), "50", null, "Activades personales", null });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "CreatedDate", "Descripcion", "PriorityTask", "Title" },
                values: new object[] { new Guid("54210ee2-e34f-4a73-87ba-5be75a47c310"), new Guid("54210ee2-e34f-4a73-87ba-5be75a47c3b6"), new DateTime(2024, 10, 4, 15, 19, 36, 278, DateTimeKind.Local).AddTicks(8922), null, 1, "pago de servivios pulico" });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "CreatedDate", "Descripcion", "PriorityTask", "Title" },
                values: new object[] { new Guid("54210ee2-e34f-4a73-87ba-5be75a47c311"), new Guid("54210ee2-e34f-4a73-87ba-5be75a47c3c4"), new DateTime(2024, 10, 4, 15, 19, 36, 278, DateTimeKind.Local).AddTicks(8939), null, 0, "terminar de ver peliculas" });

            migrationBuilder.CreateIndex(
                name: "IX_Tarea_CategoriaId",
                table: "Tarea",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarea");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
