using TelegramGPT.Services;
using TelegramGPT.EndPoints.WebHook;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ChatGPT>();
builder.Services.AddScoped<TelegramBot>();

builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapHealthChecks("/health");


app.MapMethods(WebHook.Template, WebHook.Methods, WebHook.Handle);

app.Run();