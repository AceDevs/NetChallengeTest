using MediatR;
using NetChallengeTest.Core.Commands;
using NetChallengeTest.Core.Models;
using NetChallengeTestAPI.Services.Repositories;

namespace NetChallengeTestAPI.Handlers.ProductHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductsRepository _productsRepository;
        public UpdateProductCommandHandler(IProductsRepository productsRepository)
            => _productsRepository = productsRepository;

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var category = new Product
            {
                Id = request.Id,
                ProductCode = request.ProductCode,
                ProductName = request.ProductName,
                CategoryId = request.CategoryId
            };
            await _productsRepository.Update(category);

            return category;
        }
    }
}
