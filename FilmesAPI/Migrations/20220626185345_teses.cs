using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmesAPI.Migrations
{
   public partial class teses : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.EnsureSchema(
             name: "Filme");

         migrationBuilder.CreateTable(
             name: "FILME",
             schema: "Filme",
             columns: table => new
             {
                Id = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                diretor = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                genero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                duracao = table.Column<int>(type: "int", nullable: false),
                imdb = table.Column<float>(type: "real", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_FILME", x => x.Id);
             });

         migrationBuilder.CreateTable(
             name: "SITE",
             schema: "Filme",
             columns: table => new
             {
                Id = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                nome = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                filmeId = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_SITE", x => x.Id);
                table.ForeignKey(
                       name: "FK_SITE_FILME_filmeId",
                       column: x => x.filmeId,
                       principalSchema: "Filme",
                       principalTable: "FILME",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateIndex(
             name: "IX_SITE_filmeId",
             schema: "Filme",
             table: "SITE",
             column: "filmeId");

         migrationBuilder.CreateIndex(
             name: "IX_SITE_url",
             schema: "Filme",
             table: "SITE",
             column: "url",
             unique: true);
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "SITE",
             schema: "Filme");

         migrationBuilder.DropTable(
             name: "FILME",
             schema: "Filme");
      }
   }
}
