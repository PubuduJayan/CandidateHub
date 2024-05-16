using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

// Add services to the container.
// Repository
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(CachedRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddMemoryCache();

builder.Services.AddControllers();

// CORS
const string CORS_POLICY = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS_POLICY,
                      builder =>
                      {
                          builder.AllowAnyOrigin();
                          builder.AllowAnyMethod();
                          builder.AllowAnyHeader();
                      });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CandidateHub Public API", Version = "v1" });
    c.EnableAnnotations();
  
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseCors(CORS_POLICY);
app.MapControllers();

app.Run();
