using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using DoctorWho.Db.DatabaseContext;
using DoctorWho.Web.Filters;
using FluentValidation.AspNetCore;
using DoctorWho.Web.Models;
using DoctorWho.Web.Validators;
using DoctorWho.Db.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DoctorWhoCoreDbContext>(
    dbContextOptions => dbContextOptions.UseSqlServer(
        builder.Configuration.GetConnectionString("DBConnectionString")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<DoctorRepository>();

builder.Services.AddFluentValidation();
builder.Services.AddScoped<IValidator<AuthorDto>, AuthorValidator>();
builder.Services.AddScoped<IValidator<CompanionDto>, CompanionValidator>();
builder.Services.AddScoped<IValidator<DoctorDto>, DoctorValidator>();
builder.Services.AddScoped<IValidator<EnemyDto>, EnemyValidator>();
builder.Services.AddScoped<IValidator<EpisodeDto>, EpisodeValidator>();

builder.Services
    .AddMvc(options =>
    {
        options.EnableEndpointRouting = false;
        options.Filters.Add<ValidationFilter>();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
