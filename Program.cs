var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

//Configura��o da string de conex�o
string SqlConnection = builder.Configuration.GetConnectionString("connection");
builder.Services.AddDbContext<ISPMediaDbContext>(options =>
    options.UseSqlServer((SqlConnection)));

//Injec��o de depend�ncia
builder.Services.AddScoped<UtilizadorService>();
builder.Services.AddScoped<ArtistaService>();
builder.Services.AddScoped<BandaService>();
//builder.Services.AddScoped<AlbumService>();
builder.Services.AddScoped<MusicaService>();
//builder.Services.AddScoped<GeneroService>();  
//builder.Services.AddScoped<PlaylistService>();
//builder.Services.AddScoped<CompositorService>();
//builder.Services.AddScoped<ParticipacaoService>();
builder.Services.AddScoped<ProdutoraService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
