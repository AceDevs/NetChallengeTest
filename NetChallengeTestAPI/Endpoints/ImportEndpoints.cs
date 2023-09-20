using CsvHelper.Configuration;
using CsvHelper;
using FluentValidation;
using NetChallengeTest.Core.Models;
using NetChallengeTestAPI.Services;
using System.Globalization;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Mvc;

namespace NetChallengeTestAPI.Endpoints;

public static class ImportEndpoints
{
    public static WebApplication MapImportEndpoints(this WebApplication app)
    {
        app.MapGet("importData", ImportDataFromFile);
        return app;
    }

    private static async Task<IResult> ImportDataFromFile(ICategoriesService categoriesService, IProductsService productsService, HttpRequest request)
    {
        try
        {
            var records = new List<CategoryProductRaw>();
            if (!request.Form.Files.Any())
                throw new Exception("File not provided.");
            using var reader = new StreamReader(request.Form.Files.FirstOrDefault().OpenReadStream());
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                records = csv.GetRecords<CategoryProductRaw>().ToList();
            var categories = new List<Category>();
            var products = new List<Product>();
            records.ForEach(x
            => {
                Category currentCategory;
                if (!categories.Any(c => c.CategoryCode == x.CategoryCode))
                    categories.Add(new Category { CategoryCode = x.CategoryCode, CategoryName = x.CategoryName, Products = new List<Product>() });

                currentCategory = categories.First(c => c.CategoryCode == x.CategoryCode);
                currentCategory.Products.Add(new Product { ProductCode = x.ProductCode, ProductName = x.ProductName });
            });
            var createdCategories = (List<Category>)await categoriesService.BulkCreate(categories);
            categories.ForEach(c => c.Id = createdCategories.FirstOrDefault(cc => c.CategoryCode == cc.CategoryCode).Id);
            var createdProducts = (List<Product>)await productsService.BulkCreate(
                categories.SelectMany( c => c.Products,
                        (c, p) => new Product { CategoryId = c.Id, ProductCode = p.ProductCode, ProductName = p.ProductName })
                    .ToList());

            return Results.Ok(categories);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
