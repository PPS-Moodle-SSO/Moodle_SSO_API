using Microsoft.EntityFrameworkCore;
using Moodle_SSO_API;
using Moodle_SSO_API.Repository.IRepository;
using Moodle_SSO_API.Data;
using Moodle_SSO_API.Repository;
using Moodle_SSO_API.Handlers.IHandler;
using Moodle_SSO_API.Services.Interfaces;
using Moodle_SSO_API.Services.Moodle;
using Moodle_SSO_API.Services.HTTP;
using Moodle_SSO_API.Handlers.Enterprises;
using Moodle_SSO_API.Handlers.Moodles;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrSQLServer"));
});


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder.SetIsOriginAllowed(_ => true)
                                                .WithOrigins("+")
                                                .AllowAnyMethod()
                                                .AllowAnyHeader()
                                                .AllowCredentials()
                                                );
});
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingConfig>();
});

builder.Services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();

builder.Services.AddScoped<IEnterpriseHandler, EnterpriseHandler>();
builder.Services.AddScoped<IMoodleHandler, MoodleHandler>();

builder.Services.AddHttpClient();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IMoodleService, MoodleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors("AllowSpecificOrigin");
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run(); 