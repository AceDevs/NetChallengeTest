using Microsoft.EntityFrameworkCore;
using NetChallengeTestAPI.Data;
using NetChallengeTestAPI.Endpoints;
using FluentValidation;
using NetChallengeTest.Core.Validators;
using NetChallengeTest.Core.Models;
using System.Reflection;
using NetChallengeTestAPI.Services;
using NetChallengeTestAPI.Services.Repositories;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
});


builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>()
                .AddScoped<IProductsRepository, ProductsRepository>();

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IValidator<Category>, CategoryValidator>()
                .AddScoped<IValidator<Product>, ProductValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCategoryEndpoints()
    .MapProductEndpoints()
    .MapImportEndpoints();


app.MapGet("", context =>
{
    context.Response.Redirect("./swagger/index.html", permanent: false);
    return Task.FromResult(0);
});

app.Run();