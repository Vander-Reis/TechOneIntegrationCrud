using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using PersonApi.Application;
using PersonApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Obter a configura��o do appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

// Adicionar servi�os ao cont�iner.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar a string de conex�o do SQLite
var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(connectionString));

// Registrar o PessoaService e o PessoaRepository no cont�iner de DI
builder.Services.AddScoped<PessoaService>();
builder.Services.AddScoped<PessoaRepository>();

var app = builder.Build();

// Chamar o m�todo CriarTabelaPessoas() do ApplicationContext
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    context.CriarTabelaPessoas();
}

// Configurar o pipeline de requisi��o HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
