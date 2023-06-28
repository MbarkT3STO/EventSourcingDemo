using EventSourcingDemo.Data;
using EventSourcingDemo.Data.AuditLogContext;
using EventSourcingDemo.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add MediatR
builder.Services.AddMediatR(typeof(Program).Assembly);

// Add Write DB Context
builder.Services.AddDbContext<AppWriteDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("WriteDbConnection"));
});

// Add Read DB Context
builder.Services.AddDbContext<AppReadDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("ReadDbConnection"));
});

// Add Audit Log DB Context
builder.Services.AddDbContext<AuditLogDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("AuditLogDbConnection"));
});

// Register Event Replay Service
builder.Services.AddScoped<EventReplayService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
