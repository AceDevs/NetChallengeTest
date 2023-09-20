using NetChallengeTest.Core.Models;
using Dapper;
using NetChallengeTestAPI.Data;

namespace NetChallengeTestAPI.Services.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private DapperContext _context;
        public ProductsRepository(DapperContext context)
            => _context = context;

        public async Task<IList<Product>> GetAsync(int page, int pageSize, string sortBy)
        {
            var offset = (page - 1) * pageSize;
            string commandText = $"SELECT * FROM \"Products\" ";
            if (!string.IsNullOrEmpty(sortBy))
                commandText += @"ORDER BY CASE WHEN @sortBy = 'Id ASC' THEN ""Id"" END ASC,
                                CASE WHEN @sortBy = 'Id DESC' THEN ""Id"" END DESC,
                                CASE WHEN @sortBy = 'ProductCode ASC' THEN ""ProductCode"" END ASC,
                                CASE WHEN @sortBy = 'ProductCode DESC' THEN ""ProductCode"" END DESC,
                                CASE WHEN @sortBy = 'ProductName ASC' THEN ""ProductName"" END ASC,
                                CASE WHEN @sortBy = 'ProductName DESC' THEN ""ProductName"" END DESC,
                                CASE WHEN @sortBy = 'CategoryId ASC' THEN ""CategoryId"" END ASC,
                                CASE WHEN @sortBy = 'CategoryId DESC' THEN ""CategoryId"" END DESC ";
            commandText += "LIMIT @pageSize OFFSET @offset";
            var queryArguments = new { offset, pageSize, sortBy };


            using var connection = _context.CreateConnection();
            var products = await connection.QueryAsync<Product>(commandText, queryArguments);

            return products.ToList();

        }

        public async Task<int> Add(Product product)
        {
            string queryText = $"SELECT * FROM \"Products\" WHERE \"ProductCode\" = @productCode";
            var queryArguments = new { productCode = product.ProductCode };
            using var connection = _context.CreateConnection();
            var result = await connection.QueryFirstOrDefaultAsync<Product>(queryText, queryArguments);
            if (result != null && result.Id != 0)
                throw new Exception($"That productCode [{product.ProductCode}] already exists. If you want to update the product use the PUT verbose.");

            string commandText = $"INSERT INTO \"Products\" (\"ProductCode\", \"ProductName\", \"CreationDate\", \"CategoryId\") VALUES (@productCode, @productName, @creationDate, @categoryId) RETURNING \"Id\"";

            var scalarArguments = new
            {
                productCode = product.ProductCode,
                productName = product.ProductName,
                creationDate = product.CreationDate,
                categoryId = product.CategoryId,
            };

            return await connection.ExecuteScalarAsync<int>(commandText, scalarArguments);
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            string commandText = $"SELECT * FROM \"Products\" WHERE \"Id\" = @id";
            var queryArguments = new { id };

            using var connection = _context.CreateConnection();
            var result = await connection.QueryFirstAsync<Product>(commandText, queryArguments);
            return result;
        }

        public async Task<Product> FindByNameAsync(string productName)
        {
            string commandText = $"SELECT * FROM \"Products\" WHERE \"ProductName\" = @productName";
            var queryArguments = new { productName };

            using var connection = _context.CreateConnection();
            var products = await connection.QueryFirstAsync<Product>(commandText, queryArguments);

            return products;
        }

        public async Task<Product> FindByCodeAsync(string productCode)
        {
            string commandText = $"SELECT * FROM \"Products\" WHERE \"ProductCode\" = @productCode";
            var queryArguments = new { productCode };

            using var connection = _context.CreateConnection();
            var products = await connection.QueryFirstAsync<Product>(commandText, queryArguments);

            return products;
        }

        public async Task<bool> Update(Product product)
        {
            var commandText = $@"UPDATE ""Products""
                SET ""ProductCode"" = @productCode, ""ProductName"" = @productName
                WHERE ""Id"" = @id";

            var queryArgs = new
            {
                id = product.Id,
                productCode = product.ProductCode,
                productName = product.ProductName,
            };

            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(commandText, queryArgs) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            string commandText = $"DELETE FROM \"Products\" WHERE \"Id\" = @id";
            var queryArguments = new { id };

            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(commandText, queryArguments) > 0;
        }
    }
}
