using BrinquedosAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar o DbContext para usar Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar os controllers ao pipeline
builder.Services.AddControllers();

// Configurar o Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Construir o aplicativo
var app = builder.Build();

// Configurar o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    // Habilitar o Swagger e Swagger UI em ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirecionar HTTP para HTTPS
app.UseHttpsRedirection();

// Habilitar autorização (se necessário)
app.UseAuthorization();

// Mapear os controllers
app.MapControllers();

// Executar o aplicativo
app.Run();