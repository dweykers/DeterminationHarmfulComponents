using Core;
using DbAccess;
using PhotoEditingService;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.SetCustomSettings();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InstallModule<ServicesModule>(builder.Configuration);
builder.Services.InstallModule<DbAccessModule>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
