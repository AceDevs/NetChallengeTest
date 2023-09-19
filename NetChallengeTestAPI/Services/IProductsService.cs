using NetChallengeTest.Core.Models;

namespace NetChallengeTestAPI.Services
{
    public interface IProductsService
    {
        Task<Product> Create(Product product);
        Task<bool> Delete(int id);
        Task<Product?> GetById(int id);
        Task<Product?> GetByName(string name);
        Task<IList<Product>> GetProducts(int page, int pageSize, string sortBy);
        Task<Product> Update(Product product);
        Task<IList<Product>> BulkCreate(List<Product> products);
        Task<Product> FindProductByCode(string productCode);
    }
}