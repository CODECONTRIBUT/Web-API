using Microsoft.EntityFrameworkCore;
using Web_API.Interfaces;
using Web_API.MappingProfiles;
using Web_API.Models;
using Web_API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<storeContext>(
    options => options.UseMySql(connStr, ServerVersion.AutoDetect(connStr))); //Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql")

builder.Services.AddCors(options =>
{
    //React app
    options.AddPolicy("ReactApp", policybuilder =>
    {
        policybuilder.WithOrigins("https://youto.azurewebsites.net");
        policybuilder.AllowAnyHeader();
        policybuilder.AllowAnyMethod();
        policybuilder.AllowCredentials();
    });

    //React Native app next
});

builder.Services.AddAutoMapper(typeof(StoreSystemProfile));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IScreenshotRepository, ScreenshotRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<ITrailerRepository, TrailerRepository>();
builder.Services.AddScoped<IParentPlatformRepository, ParentPlatformRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("ReactApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//Plans:
//
//Congrats. Yes!!!!!!  Integration is done successfully. Just need handle details.                          23/04/2024
//Integration test has all done!! All pages run well. Would deploy whole end-to-end to AWS next.            24/04/2024
//After that, add Post/Put/Delete on front-end.                                                             next phrase
