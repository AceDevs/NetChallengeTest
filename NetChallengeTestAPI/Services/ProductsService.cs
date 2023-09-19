using MediatR;
using NetChallengeTest.Core.Commands;
using NetChallengeTest.Core.Models;
using NetChallengeTest.Core.Queries;

namespace NetChallengeTestAPI.Services
{
    public class ProductsService : IProductsService
    {
        public ProductsService(IMediator mediator) => _mediator = mediator;
        private readonly IMediator _mediator;

        public async Task<Product> Create(Product product)
        {
            var command = new CreateProductCommand
            {
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId
            };
            Product response = await _mediator.Send(command);
            return response;
        }

        public async Task<bool> Delete(int id)
        {
            var command = new DeleteProductCommand { Id = id };
            return await _mediator.Send(command);
        }

        public async Task<IList<Product>> GetProducts(int page, int pageSize, string sortBy)
        {
            var command = new GetProductsQuery
            {
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy
            };
            IList<Product> products = await _mediator.Send(command);
            return products;
        }

        public async Task<Product?> GetById(int id)
        {
            var command = new GetProductByIdQuery { Id = id };
            Product product = await _mediator.Send(command);
            return product;
        }

        public async Task<Product?> GetByName(string name)
        {
            var command = new GetProductByNameQuery { Name = name };
            Product product = await _mediator.Send(command);
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            var command = new UpdateProductCommand()
            {
                Id = product.Id,
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId
            };
            Product updatedProduct = await _mediator.Send(command);
            return updatedProduct;
        }

        public async Task<Product> FindProductByCode(string productCode)
        {
            var command = new GetProductByProductCodeQuery { ProductCode = productCode };
            Product product = await _mediator.Send(command);
            return product;
        }

        public async Task<IList<Product>> BulkCreate(List<Product> products)
        {
            var createdProducts = new List<Product>();
            foreach (var product in products)
                createdProducts.Add(await Create(product));

            return createdProducts;
        }
    }
}
