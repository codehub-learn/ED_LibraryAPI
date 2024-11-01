using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ED_LibraryAPI.Data;
using ED_LibraryAPI.Domain;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using ED_LibraryAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddDbContext<LibContext>(options => 
    options
    .UseSqlServer(builder.Configuration.GetConnectionString("ED_LibraryAPI"))
    );
//builder.Services.Configure<JsonOptions>(options =>
//    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
//builder.Services.Configure<JsonOptions>(options =>
//    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<IBookService, BookService>();

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
