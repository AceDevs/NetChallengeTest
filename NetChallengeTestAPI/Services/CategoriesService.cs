using MediatR;
using NetChallengeTest.Core.Models;
using NetChallengeTest.Core.Commands;
using NetChallengeTest.Core.Queries;

namespace NetChallengeTestAPI.Services;

public class CategoriesService : ICategoriesService
{
    public CategoriesService(IMediator mediator) => _mediator = mediator;
    private readonly IMediator _mediator;

    public async Task<Category> Create(Category category)
    {
        var command = new CreateCategoryCommand
        {
            CategoryCode = category.CategoryCode,
            CategoryName = category.CategoryName
        };
        Category response = await _mediator.Send(command);
        return response;
    }

    public async Task<bool> Delete(int id)
    {
        var command = new DeleteCategoryCommand { Id = id };
        return await _mediator.Send(command);
    }

    public async Task<IList<Category>> GetCategories(int page, int pageSize, string sortBy)
    {
        var command = new GetCategoriesQuery
        {
            Page = page,
            PageSize = pageSize,
            SortBy = sortBy
        };
        IList<Category> categories = await _mediator.Send(command);
        return categories;
    }

    public async Task<Category?> GetById(int id)
    {
        var command = new GetCategoryByIdQuery { Id = id };
        Category category = await _mediator.Send(command);
        return category;
    }

    public async Task<Category?> GetByName(string name)
    {
        var command = new GetCategoryByNameQuery { Name = name };
        Category category = await _mediator.Send(command);
        return category;
    }

    public async Task<Category> Update(Category category)
    {
        var command = new UpdateCategoryCommand()
        {
            Id = category.Id,
            CategoryCode = category.CategoryCode,
            CategoryName = category.CategoryName
        };
        Category updatedCategory = await _mediator.Send(command);
        return updatedCategory;
    }

    public async Task<IList<Category>> BulkCreate(IList<Category> categories)
    {
        var createdCategories = new List<Category>();
        foreach (var category in categories)
            createdCategories.Add(await Create(category));

        return createdCategories;
    } 
}
