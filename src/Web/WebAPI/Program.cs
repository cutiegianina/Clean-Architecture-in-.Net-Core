using Infrastructure;
using Infrastructure.Data;
using WebAPI.MiddleWares.LoggerConfiguration;
using WebAPI.Middlewares;
using System.Reflection;
using Application;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddApplicationLogs();

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;
Assembly presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;

// Add services to the container.

services.AddControllers()
		.AddApplicationPart(presentationAssembly);
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddInfrastructureServices(configuration);
services.AddApplicationServices();
services.AddMiddlewareServices();

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

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();