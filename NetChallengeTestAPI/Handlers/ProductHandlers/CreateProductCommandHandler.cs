using MediatR;
using NetChallengeTest.Core.Commands;
using NetChallengeTest.Core.Models;
using NetChallengeTestAPI.Services.Repositories;

namespace NetChallengeTestAPI.Handlers.ProductHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductsRepository _productsRepository;
        public CreateProductCommandHandler(IProductsRepository productsRepository) 
            => _productsRepository = productsRepository;

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = new Product
            {
                ProductCode = request.ProductCode,
                ProductName = request.ProductName,
                CategoryId = request.CategoryId,
                CreationDate = DateTime.Now
            };

            var newId = await _productsRepository.Add(newProduct);
            newProduct.Id = newId;
            return newProduct;
        }
    }
}
