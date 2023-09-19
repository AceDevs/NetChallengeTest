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

app.UseHttpsRedirection();

app.MapCategoryEndpoints()
    .MapProductEndpoints();


app.MapGet("importData", async (ICategoriesService categoriesService, IProductsService productsService, HttpContext context) =>
{
    try
    {
        var records = new List<CategoryProductRaw>();
        if (!context.Request.Form.Files.Any())
            throw new Exception("File not provided.");
        using var reader = new StreamReader(context.Request.Form.Files.FirstOrDefault().OpenReadStream());
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            records = csv.GetRecords<CategoryProductRaw>().ToList();
        var categories = new List<Category>();
        var products = new List<Product>();
        records.ForEach(x =>
        {
            Category currentCategory;
            if (!categories.Any(c => c.CategoryCode == x.CategoryCode))
                categories.Add(new Category { CategoryCode = x.CategoryCode, CategoryName = x.CategoryName, Products = new List<Product>() });

            currentCategory = categories.First(c => c.CategoryCode == x.CategoryCode);
            currentCategory.Products.Add(new Product { ProductCode = x.ProductCode, ProductName = x.ProductName });
        });
        var createdCategories = (List<Category>)await categoriesService.BulkCreate(categories);
        categories.ForEach(c => c.Id = createdCategories.FirstOrDefault(cc => c.CategoryCode == cc.CategoryCode).Id);
        var createdProducts = (List<Product>)await productsService
                                .BulkCreate(categories
                                    .SelectMany(
                                        c => c.Products,
                                        (c, p) => new Product { CategoryId = c.Id, ProductCode = p.ProductCode, ProductName = p.ProductName })
                                    .ToList());

        return Results.Ok(categories);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();