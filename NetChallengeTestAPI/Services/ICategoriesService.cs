using NetChallengeTest.Core.Models;

namespace NetChallengeTestAPI.Services
{
    public interface ICategoriesService
    {
        Task<IList<Category>> BulkCreate(IList<Category> categories);
        Task<Category> Create(Category category);
        Task<bool> Delete(int id);
        Task<Category?> GetById(int id);
        Task<Category?> GetByName(string name);
        Task<IList<Category>> GetCategories(int page, int pageSize, string sortBy);
        Task<Category> Update(Category category);
    }
}