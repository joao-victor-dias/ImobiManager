using ImobiManager.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ImobiManager API",
        Version = "v1",
        Description = "Api para gerenciamento de vendas e reservas de apartamentos. Permite registrar, atualizar e consultar informações sobre clientes, apartamentos, reservas e transações de vendas."
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ImobiManager API v1");
        options.RoutePrefix = string.Empty;
    });
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (dbContext.Database.EnsureCreated())
    {
        dbContext.Database.Migrate();
    }
}

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//    try
//    {
//        // Verifica se existem migrations pendentes
//        if (!dbContext.Database.GetMigrations().Any())
//        {
//            // Gera uma migration inicial automaticamente
//            var process = new Process
//            {
//                StartInfo = new ProcessStartInfo
//                {
//                    FileName = "dotnet",
//                    Arguments = "ef migrations add AutoMigration",
//                    RedirectStandardOutput = true,
//                    RedirectStandardError = true,
//                    UseShellExecute = false,
//                    CreateNoWindow = true
//                }
//            };

//            process.Start();
//            process.WaitForExit();
//        }

//        // Aplica as migrations (criando tabelas se necessário)
//        dbContext.Database.Migrate();
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Erro ao aplicar migrations: {ex.Message}");
//    }
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

