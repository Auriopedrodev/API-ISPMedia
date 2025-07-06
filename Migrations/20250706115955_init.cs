using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISPMediaAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bandas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Biografia = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    Fim = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bandas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lancamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FichaTecnica = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DataLancamento = table.Column<DateOnly>(type: "date", nullable: false),
                    TipoLancamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Capa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lancamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtoras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UltimoNome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artistas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ProdutoraId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artistas_Bandas_BandaId",
                        column: x => x.BandaId,
                        principalTable: "Bandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Artistas_Produtoras_ProdutoraId",
                        column: x => x.ProdutoraId,
                        principalTable: "Produtoras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CaminhoMedia = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Tamanho = table.Column<long>(type: "bigint", nullable: false),
                    TipoMedia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Letra = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    TipoGeneroId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LancamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Formato = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Duracao = table.Column<int>(type: "int", nullable: true),
                    Resolucao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medias_Artistas_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Artistas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Medias_Generos_TipoGeneroId",
                        column: x => x.TipoGeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Medias_Lancamentos_LancamentoId",
                        column: x => x.LancamentoId,
                        principalTable: "Lancamentos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MediaParticipacao",
                columns: table => new
                {
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaParticipacao", x => new { x.MediaId, x.ParticipacaoId });
                    table.ForeignKey(
                        name: "FK_MediaParticipacao_Artistas_ParticipacaoId",
                        column: x => x.ParticipacaoId,
                        principalTable: "Artistas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MediaParticipacao_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MusicaCompositor",
                columns: table => new
                {
                    CompositorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicaCompositor", x => new { x.CompositorId, x.MusicaId });
                    table.ForeignKey(
                        name: "FK_MusicaCompositor_Artistas_CompositorId",
                        column: x => x.CompositorId,
                        principalTable: "Artistas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MusicaCompositor_Medias_MusicaId",
                        column: x => x.MusicaId,
                        principalTable: "Medias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlaylistMedia",
                columns: table => new
                {
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistMedia", x => new { x.MediaId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_PlaylistMedia_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlaylistMedia_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artistas_BandaId",
                table: "Artistas",
                column: "BandaId");

            migrationBuilder.CreateIndex(
                name: "IX_Artistas_Nome",
                table: "Artistas",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Artistas_ProdutoraId",
                table: "Artistas",
                column: "ProdutoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Bandas_Nome",
                table: "Bandas",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Generos_Nome",
                table: "Generos",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_MediaParticipacao_ParticipacaoId",
                table: "MediaParticipacao",
                column: "ParticipacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_AutorId",
                table: "Medias",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_LancamentoId",
                table: "Medias",
                column: "LancamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_TipoGeneroId",
                table: "Medias",
                column: "TipoGeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicaCompositor_MusicaId",
                table: "MusicaCompositor",
                column: "MusicaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistMedia_PlaylistId",
                table: "PlaylistMedia",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtoras_Nome",
                table: "Produtoras",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_Email",
                table: "Utilizadores",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_Username",
                table: "Utilizadores",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaParticipacao");

            migrationBuilder.DropTable(
                name: "MusicaCompositor");

            migrationBuilder.DropTable(
                name: "PlaylistMedia");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Artistas");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Lancamentos");

            migrationBuilder.DropTable(
                name: "Bandas");

            migrationBuilder.DropTable(
                name: "Produtoras");
        }
    }
}
