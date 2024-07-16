using DailyReporter.Api;
using DailyReporter.Application.DependencyInjection;
using DailyReporter.DAL.DependendyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwagger();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("swagger/v1/swagger.json", "DailyReporter Swagger v1.0");
		c.SwaggerEndpoint("swagger/v2/swagger.json", "DailyReporter Swagger v2.0");
		c.RoutePrefix = string.Empty;
	});
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
