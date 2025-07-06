namespace ISPMediaAPI.Context;

public class ISPMediaDbContext : DbContext
{
    public ISPMediaDbContext (DbContextOptions <ISPMediaDbContext> options) : base(options) 
    { 

    }
    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Autor> Autores { get; set; }
    public DbSet<Banda> Bandas { get; set; }
    public DbSet<Compositor> Compositores { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<Lancamento> Lancamentos { get; set; }
    public DbSet<Media> Medias { get; set; }
    public DbSet<Musica> Musicas { get; set; }
    public DbSet<Participacao> Participacoes { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Produtora> Produtoras { get; set; }
    public DbSet<Utilizador> Utilizadores { get; set; }
    public DbSet<Video> Videos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração do relacionamento many-to-many entre Musica e Compositor
        modelBuilder.Entity<Musica>()
            .HasMany(m => m.Compositores)
            .WithMany(c => c.Musicas)
            .UsingEntity(
                "MusicaCompositor",
                l => l.HasOne(typeof(Compositor)).WithMany().HasForeignKey("CompositorId").OnDelete(DeleteBehavior.NoAction),
                r => r.HasOne(typeof(Musica)).WithMany().HasForeignKey("MusicaId").OnDelete(DeleteBehavior.NoAction)
            );

        // Configuração do relacionamento many-to-many entre Media e Participacao
        modelBuilder.Entity<Media>()
            .HasMany(m => m.Participacoes)
            .WithMany(p => p.Medias)
            .UsingEntity(
                "MediaParticipacao",
                l => l.HasOne(typeof(Participacao)).WithMany().HasForeignKey("ParticipacaoId").OnDelete(DeleteBehavior.NoAction),
                r => r.HasOne(typeof(Media)).WithMany().HasForeignKey("MediaId").OnDelete(DeleteBehavior.NoAction)
            );

        // Configuração do relacionamento many-to-many entre Playlist e Media
        modelBuilder.Entity<Playlist>()
            .HasMany(p => p.Medias)
            .WithMany()
            .UsingEntity(
                "PlaylistMedia",
                l => l.HasOne(typeof(Media)).WithMany().HasForeignKey("MediaId").OnDelete(DeleteBehavior.NoAction),
                r => r.HasOne(typeof(Playlist)).WithMany().HasForeignKey("PlaylistId").OnDelete(DeleteBehavior.NoAction)
            );

        // Configuração de relacionamentos one-to-many para evitar ciclos de cascade
        modelBuilder.Entity<Media>()
            .HasOne(m => m.Autor)
            .WithMany()
            .HasForeignKey(m => m.AutorId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Musica>()
            .HasOne(m => m.TipoGenero)
            .WithMany(g => g.Musicas)
            .HasForeignKey(m => m.TipoGeneroId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Musica>()
            .HasOne(m => m.Lancamento)
            .WithMany(l => l.Musicas)
            .HasForeignKey(m => m.LancamentoId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Autor>()
            .HasOne(a => a.Produtora)
            .WithMany(p => p.Autores)
            .HasForeignKey(a => a.ProdutoraId)
            .OnDelete(DeleteBehavior.NoAction);

        // Configuração de índices para melhor performance
        modelBuilder.Entity<Utilizador>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Utilizador>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<Artista>()
            .HasIndex(a => a.Nome);

        modelBuilder.Entity<Genero>()
            .HasIndex(g => g.Nome);

        modelBuilder.Entity<Produtora>()
            .HasIndex(p => p.Nome);

        modelBuilder.Entity<Banda>()
            .HasIndex(b => b.Nome);
    }
}