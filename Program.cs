
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var ngrokService = new NgrokService();
ngrokService.StartNgrok();

var builder = WebApplication.CreateBuilder(args);

// 🔹 Adiciona suporte a controladores para a API do chatbot
builder.Services.AddControllers();

// 🔹 Registra a classe ClienteRepository para injeção de dependência
builder.Services.AddSingleton<IClienteRepository, ClienteRepository>();
builder.Services.AddControllers().AddApplicationPart(typeof(WebhookController).Assembly);

var app = builder.Build();

// 🔹 Habilita o roteamento para os controladores
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
