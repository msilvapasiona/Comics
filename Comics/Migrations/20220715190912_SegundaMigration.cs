using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comics.Migrations
{
    public partial class SegundaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "añoNacimiento",
                table: "Autores",
                newName: "AñoNacimiento");

            migrationBuilder.AddColumn<int>(
                name: "EditorialId",
                table: "Comics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Editoriales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editoriales", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comics_EditorialId",
                table: "Comics",
                column: "EditorialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comics_Editoriales_EditorialId",
                table: "Comics",
                column: "EditorialId",
                principalTable: "Editoriales",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comics_Editoriales_EditorialId",
                table: "Comics");

            migrationBuilder.DropTable(
                name: "Editoriales");

            migrationBuilder.DropIndex(
                name: "IX_Comics_EditorialId",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "EditorialId",
                table: "Comics");

            migrationBuilder.RenameColumn(
                name: "AñoNacimiento",
                table: "Autores",
                newName: "añoNacimiento");
        }
    }
}
