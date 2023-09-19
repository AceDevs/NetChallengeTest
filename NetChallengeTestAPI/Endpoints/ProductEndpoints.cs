using FluentValidation;
using NetChallengeTest.Core.Models;
using NetChallengeTestAPI.Services;

namespace NetChallengeTestAPI.Endpoints;

public static class ProductEndpoints
{
    public static WebApplication MapProductEndpoints(this WebApplication app)
    {
        app.MapGet(pattern: "products", GetProducts);
        app.MapGet("products/id/{id}", GetProductById);
        app.MapGet("products/name/{productName}", GetProductByName);
        app.MapGet("products/productcode/{productCode}", GetProductByProductCode);
        app.MapPost("products", CreateProduct);
        app.MapPut("products", UpdateProduct);
        app.MapDelete("products", DeleteProduct);
        return app;
    }

    public static async Task<IResult> GetProducts(IProductsService productsService, int page = 1, int pageSize = 10, string sortBy = null)
    {
        try
        {
            IList<Product>? existingProducts = await productsService.GetProducts(page, pageSize, sortBy).ConfigureAwait(false);
            return existingProducts != null ? Results.Ok(existingProducts) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> GetProductById(int id, IProductsService productsService)
    {
        try
        {
            Product? existingProduct = await productsService.GetById(id).ConfigureAwait(false);
            return existingProduct != null ? Results.Ok(existingProduct) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> GetProductByName(string productName, IProductsService productsService)
    {
        try
        {
            Product? existingProduct = await productsService.GetByName(productName).ConfigureAwait(false);
            return existingProduct != null ? Results.Ok(existingProduct) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> CreateProduct(Product product, IProductsService productsService, IValidator<Product> validator)
    {
        try
        {
            var result = await validator.ValidateAsync(product);
            if (!result.IsValid)
                return Results.ValidationProblem(result.ToDictionary());

            return Results.Ok(await productsService.Create(product).ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> UpdateProduct(Product product, IProductsService productsService, IValidator<Product> validator)
    {
        try
        {
            var result = await validator.ValidateAsync(product);
            if (!result.IsValid)
                return Results.ValidationProblem(result.ToDictionary());

            return Results.Ok(await productsService.Update(product).ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> DeleteProduct(int id, IProductsService productsService)
    {
        try
        {
            return await productsService.Delete(id).ConfigureAwait(false)
                ? Results.Ok()
                : Results.BadRequest();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> GetProductByProductCode(string productCode, IProductsService productsService)
    {
        try
        {
            Product? existingProduct = await productsService.FindProductByCode(productCode).ConfigureAwait(false);
            return existingProduct != null ? Results.Ok(existingProduct) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}
