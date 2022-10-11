using CoachAssistent.AutomapperBootstrapper;
using CoachAssistent.AutomapperBootstrapper.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using CoachAssistent.Managers.Helpers;

var builder = WebApplication.CreateBuilder(args);

JwtHelper jwtHelper = new(builder.Configuration);


// Add services to the container.
builder.Services.AddCors(options => options.AddDefaultPolicy(cfg => cfg.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
//var config = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfiles(MapperService.Profiles));
builder.Services.AddAutoMapper(typeof(ExerciseProfile));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CoachAssistent.Data.CoachAssistentDbContext>(
    options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options =>
        {
            options.TokenValidationParameters = jwtHelper.TokenValidationParameters;
        });

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
