using Infrastructure;
using Application;
using WebAPI.Loggers;
using Application.Middlewares;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddApplicationLogs();

// Add services to the container.
var configuration = builder.Configuration;
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddInfrastructureServices(configuration);
services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	await app.InitializeDatabaseAsync();
	app.UseSwagger();
	app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseHealthChecks("/health");

app.UseApplicationLogger();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
