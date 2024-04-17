using Application;
using Infrastructure;


var builder = WebApplication.CreateBuilder(args);

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
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
