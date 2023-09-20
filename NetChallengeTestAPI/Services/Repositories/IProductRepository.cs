using NetChallengeTest.Core.Models;

namespace NetChallengeTestAPI.Services.Repositories
{
    public interface IProductsRepository
    {
        Task<int> Add(Product product);
        Task<bool> Delete(int id);
        Task<Product> FindByCodeAsync(string productCode);
        Task<Product> FindByIdAsync(int id);
        Task<Product> FindByNameAsync(string productName);
        Task<IList<Product>> GetAsync(int page, int pageSize, string sortBy);
        Task<bool> Update(Product product);
    }
}