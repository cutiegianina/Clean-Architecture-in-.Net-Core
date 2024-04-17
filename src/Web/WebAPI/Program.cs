using Infrastructure;
using Application;
using Application.LoggerConfiguration;
using Application.Middlewares;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddApplicationLogs();

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;
var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;

// Add services to the container.
services.AddControllers()
		.AddApplicationPart(presentationAssembly);
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