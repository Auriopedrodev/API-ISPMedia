namespace ISPMediaAPI.Context;

public class ISPMediaDbContext : DbContext
{
    public ISPMediaDbContext (DbContextOptions <ISPMediaDbContext> options) : base(options) 
    { 

    }

    public DbSet<Album> Albums { get; set; }
    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Autor> Autores { get; set; }
    public DbSet<Banda> Bandas { get; set; }
    public DbSet<Compositor> Compositores { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<Media> Medias { get; set; }
    public DbSet<Musica> Musicas { get; set; }
    public DbSet<Participacao> Participacoes { get; set; }
    public DbSet<Produtora> Produtoras { get; set; }
    public DbSet<Utilizador> Utilizadores { get; set; }
    public DbSet<Video> Videos { get; set; }
}