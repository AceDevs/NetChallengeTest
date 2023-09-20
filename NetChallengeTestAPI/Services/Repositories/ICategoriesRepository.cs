using NetChallengeTest.Core.Models;

namespace NetChallengeTestAPI.Services.Repositories
{
    public interface ICategoriesRepository
    {
        Task<int> Add(Category category);
        Task<bool> Delete(int id);
        Task<Category> FindByIdAsync(int id);
        Task<Category> FindByNameAsync(string categoryName);
        Task<IList<Category>> GetAsync(int page, int pageSize, string sortBy);
        Task<bool> Update(Category category);
    }
}