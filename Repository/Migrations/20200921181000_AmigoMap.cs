using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AmigoMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Estado_EstadoId",
                table: "Pessoa");

            migrationBuilder.CreateTable(
                name: "Amigo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 500, nullable: false),
                    Telefone = table.Column<string>(maxLength: 200, nullable: false),
                    AmigoPessoaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amigo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amigo_Pessoa_AmigoPessoaId",
                        column: x => x.AmigoPessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Bandeira = table.Column<string>(nullable: true),
                    PaisId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estados_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amigo_AmigoPessoaId",
                table: "Amigo",
                column: "AmigoPessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_PaisId",
                table: "Estados",
                column: "PaisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Estados_EstadoId",
                table: "Pessoa",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Estados_EstadoId",
                table: "Pessoa");

            migrationBuilder.DropTable(
                name: "Amigo");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Estado_EstadoId",
                table: "Pessoa",
                column: "EstadoId",
                principalTable: "Estado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
