using FluentValidation;
using NetChallengeTest.Core.Models;
using NetChallengeTestAPI.Services;

namespace NetChallengeTestAPI.Endpoints;

public static class CategoryEndpoints
{
    public static WebApplication MapCategoryEndpoints(this WebApplication app)
    {
        app.MapGet("categories", GetCategories);
        app.MapGet("categories/id/{id}", GetCategoryById);
        app.MapGet("categories/name/{categoryName}", GetCategoryByName);
        app.MapPost("categories", CreateCategory);
        app.MapPut("categories", UpdateCategory);
        app.MapDelete("categories", DeleteCategory);
        return app;
    }

    public static async Task<IResult> GetCategories(ICategoriesService categoriesService, int page = 1, int pageSize = 10, string sortBy = null)
    {
        try
        {
            IList<Category>? existingCategories = await categoriesService.GetCategories(page, pageSize, sortBy).ConfigureAwait(false);
            return existingCategories != null ? Results.Ok(existingCategories) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> GetCategoryById(int id, ICategoriesService categoriesService)
    {
        try
        {
            Category? existingCategory = await categoriesService.GetById(id).ConfigureAwait(false);
            return existingCategory != null ? Results.Ok(existingCategory) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> GetCategoryByName(string categoryName, ICategoriesService categoriesService)
    {
        try
        {
            Category? existingCategory = await categoriesService.GetByName(categoryName).ConfigureAwait(false);
            return existingCategory != null ? Results.Ok(existingCategory) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> CreateCategory(Category category, ICategoriesService categoriesService, IValidator<Category> validator)
    {
        try
        {
            var result = await validator.ValidateAsync(category);
            if (!result.IsValid)
                return Results.ValidationProblem(result.ToDictionary());

            return Results.Ok(await categoriesService.Create(category).ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> UpdateCategory(Category category, ICategoriesService categoriesService, IValidator<Category> validator)
    {
        try
        {
            var result = await validator.ValidateAsync(category);
            if (!result.IsValid)
                return Results.ValidationProblem(result.ToDictionary());

            return Results.Ok(await categoriesService.Update(category).ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> DeleteCategory(int id, ICategoriesService categoriesService)
    {
        try
        {
            return await categoriesService.Delete(id).ConfigureAwait(false)
                ? Results.Ok() 
                : Results.BadRequest();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}
