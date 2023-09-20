using NetChallengeTest.Core.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using NetChallengeTestAPI.Data;

namespace NetChallengeTestAPI.Services.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private DapperContext _context;
        public CategoriesRepository(DapperContext context)
            => _context = context;

        public async Task<IList<Category>> GetAsync(int page, int pageSize, string sortBy)
        {
            var offset = (page - 1) * pageSize;
            string commandText = $"SELECT * FROM \"Categories\" ";
            if (!string.IsNullOrEmpty(sortBy))
                commandText += @"ORDER BY CASE WHEN @sortBy = 'Id ASC' THEN ""Id"" END ASC,
                                CASE WHEN @sortBy = 'Id DESC' THEN ""Id"" END DESC,
                                CASE WHEN @sortBy = 'CategoryCode ASC' THEN ""CategoryCode"" END ASC,
                                CASE WHEN @sortBy = 'CategoryCode DESC' THEN ""CategoryCode"" END DESC,
                                CASE WHEN @sortBy = 'CategoryName ASC' THEN ""CategoryName"" END ASC,
                                CASE WHEN @sortBy = 'CategoryName DESC' THEN ""CategoryName"" END DESC ";
            commandText += "LIMIT @pageSize OFFSET @offset ";
            var queryArguments = new { offset, pageSize, sortBy };

            using var connection = _context.CreateConnection();
            var categories = await connection.QueryAsync<Category>(commandText, queryArguments);

            return categories.ToList();

        }

        public async Task<int> Add(Category category)
        {
            string queryText = $"SELECT * FROM \"Categories\" WHERE \"CategoryCode\" = @categoryCode;";
            var queryArguments = new { categoryCode = category.CategoryCode };

            using var connection = _context.CreateConnection();
            var result = await connection.QueryFirstOrDefaultAsync<Category>(queryText, queryArguments);
            if (result != null && result.Id != 0)
                throw new Exception($"The categoryCode [{category.CategoryCode}] already exists. If you want to update the category use the PUT verbose.");

            string commandText = $"INSERT INTO \"Categories\" (\"CategoryCode\", \"CategoryName\", \"CreationDate\") VALUES (@categoryCode, @categoryName, @creationDate) RETURNING \"Id\";";

            var scalarArguments = new
            {
                categoryCode = category.CategoryCode,
                categoryName = category.CategoryName,
                creationDate = category.CreationDate
            };

            return await connection.ExecuteScalarAsync<int>(commandText, scalarArguments);
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            string commandText = $"SELECT * FROM \"Categories\" WHERE \"Id\" = @id";
            var queryArguments = new { id };

            using var connection = _context.CreateConnection();
            var result = await connection.QueryFirstAsync<Category>(commandText, queryArguments);
            return result;
        }

        public async Task<Category> FindByNameAsync(string categoryName)
        {
            string commandText = $"SELECT * FROM \"Categories\" WHERE \"CategoryName\" = @categoryName";
            var queryArguments = new { categoryName };

            using var connection = _context.CreateConnection();
            var categories = await connection.QueryFirstAsync<Category>(commandText, queryArguments);

            return categories;
        }

        public async Task<bool> Update(Category category)
        {
            var commandText = $@"UPDATE ""Categories""
                SET ""CategoryCode"" = @categoryCode, ""CategoryName"" = @categoryName
                WHERE ""Id"" = @id";

            var queryArgs = new
            {
                id = category.Id,
                categoryCode = category.CategoryCode,
                categoryName = category.CategoryName,
            };

            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(commandText, queryArgs) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            string commandText = $"DELETE FROM \"Categories\" WHERE \"Id\" = @id";
            var queryArguments = new { id };

            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(commandText, queryArguments) > 0;

        }
    }
}
