using Microsoft.EntityFrameworkCore;
using Web_API.MappingProfiles;
using Web_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<storeContext>(
    options => options.UseMySql(connStr, ServerVersion.AutoDetect(connStr))); //Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql")

builder.Services.AddAutoMapper(typeof(StoreSystemProfile));

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
