using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISPMediaAPI.Migrations
{
    /// <inheritdoc />
    public partial class novaActualização : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artistas_Bandas_BandaId",
                table: "Artistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Artistas_Musicas_MusicaId",
                table: "Artistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Artistas_Musicas_MusicaId1",
                table: "Artistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Artistas_Produtoras_ProdutoraId",
                table: "Artistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Artistas_Videos_VideoId",
                table: "Artistas");

            migrationBuilder.DropTable(
                name: "Musicas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Artistas_MusicaId",
                table: "Artistas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Videos",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "MusicaId",
                table: "Artistas");

            migrationBuilder.DropColumn(
                name: "TipoArtista",
                table: "Artistas");

            migrationBuilder.RenameTable(
                name: "Videos",
                newName: "Medias");

            migrationBuilder.RenameColumn(
                name: "Descrição",
                table: "Bandas",
                newName: "Biografia");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "Artistas",
                newName: "TipoGeneroId");

            migrationBuilder.RenameColumn(
                name: "MusicaId1",
                table: "Artistas",
                newName: "MediaId");

            migrationBuilder.RenameIndex(
                name: "IX_Artistas_VideoId",
                table: "Artistas",
                newName: "IX_Artistas_TipoGeneroId");

            migrationBuilder.RenameIndex(
                name: "IX_Artistas_MusicaId1",
                table: "Artistas",
                newName: "IX_Artistas_MediaId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Artistas",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Medias",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Medias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AutorId",
                table: "Medias",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Medias",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Letra",
                table: "Medias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TipoGeneroId",
                table: "Medias",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medias",
                table: "Medias",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medias_AutorId",
                table: "Medias",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_TipoGeneroId",
                table: "Medias",
                column: "TipoGeneroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artistas_Bandas_BandaId",
                table: "Artistas",
                column: "BandaId",
                principalTable: "Bandas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Artistas_Medias_MediaId",
                table: "Artistas",
                column: "MediaId",
                principalTable: "Medias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artistas_Medias_TipoGeneroId",
                table: "Artistas",
                column: "TipoGeneroId",
                principalTable: "Medias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artistas_Produtoras_ProdutoraId",
                table: "Artistas",
                column: "ProdutoraId",
                principalTable: "Produtoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Artistas_AutorId",
                table: "Medias",
                column: "AutorId",
                principalTable: "Artistas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Generos_TipoGeneroId",
                table: "Medias",
                column: "TipoGeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artistas_Bandas_BandaId",
                table: "Artistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Artistas_Medias_MediaId",
                table: "Artistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Artistas_Medias_TipoGeneroId",
                table: "Artistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Artistas_Produtoras_ProdutoraId",
                table: "Artistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Artistas_AutorId",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Generos_TipoGeneroId",
                table: "Medias");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medias",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_AutorId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_TipoGeneroId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Artistas");

            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "Letra",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "TipoGeneroId",
                table: "Medias");

            migrationBuilder.RenameTable(
                name: "Medias",
                newName: "Videos");

            migrationBuilder.RenameColumn(
                name: "Biografia",
                table: "Bandas",
                newName: "Descrição");

            migrationBuilder.RenameColumn(
                name: "TipoGeneroId",
                table: "Artistas",
                newName: "VideoId");

            migrationBuilder.RenameColumn(
                name: "MediaId",
                table: "Artistas",
                newName: "MusicaId1");

            migrationBuilder.RenameIndex(
                name: "IX_Artistas_TipoGeneroId",
                table: "Artistas",
                newName: "IX_Artistas_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Artistas_MediaId",
                table: "Artistas",
                newName: "IX_Artistas_MusicaId1");

            migrationBuilder.AddColumn<Guid>(
                name: "MusicaId",
                table: "Artistas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoArtista",
                table: "Artistas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Videos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Videos",
                table: "Videos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaminhoMedia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Letra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tamanho = table.Column<long>(type: "bigint", nullable: false),
                    TipoMedia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musicas_Categorias_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artistas_MusicaId",
                table: "Artistas",
                column: "MusicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Musicas_GeneroId",
                table: "Musicas",
                column: "GeneroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artistas_Bandas_BandaId",
                table: "Artistas",
                column: "BandaId",
                principalTable: "Bandas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artistas_Musicas_MusicaId",
                table: "Artistas",
                column: "MusicaId",
                principalTable: "Musicas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artistas_Musicas_MusicaId1",
                table: "Artistas",
                column: "MusicaId1",
                principalTable: "Musicas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artistas_Produtoras_ProdutoraId",
                table: "Artistas",
                column: "ProdutoraId",
                principalTable: "Produtoras",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artistas_Videos_VideoId",
                table: "Artistas",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id");
        }
    }
}
