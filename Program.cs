using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using QuizAPI.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Production
//DotNetEnv.Env.Load();
//builder.Services.AddDbContext<QuizDbContext>(options =>
//options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION")));

//dev connection
builder.Services.AddDbContext<QuizDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CONNECTION"));
});

var app = builder.Build();

app.UseCors(options =>
options.WithOrigins("https://moviequizfront.azurewebsites.net/")
    .AllowAnyMethod()
    .AllowAnyHeader());

/*app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Images")),
    RequestPath = "/Images"
});*/

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();
/* to hit the defaults page */
//app.UseDefaultFiles();

//app.UseStaticFiles();

//app.UseRouting();

/*app.UseHttpsRedirection();*/
app.UseAuthorization();

app.MapControllers();

app.Run();
