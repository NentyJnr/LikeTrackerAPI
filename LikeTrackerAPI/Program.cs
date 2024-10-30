using LikeTrackerAPI.Commons.Extensions;
using LikeTrackerAPI.Data;
using LikeTrackerAPI.Implementations;
using LikeTrackerAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddJwtAuthentication(builder.Configuration);


builder.Services.AddDbContext<LikeTrackerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); 
builder.Services.AddScoped<IArticleService, ArticleService>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Like Tracker API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Like Tracker API V1");
    c.RoutePrefix = string.Empty; // Set to empty string to serve Swagger UI at the app's root
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
