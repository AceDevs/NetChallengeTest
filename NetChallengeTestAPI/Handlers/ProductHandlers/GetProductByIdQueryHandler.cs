using MediatR;
using NetChallengeTest.Core.Models;
using NetChallengeTest.Core.Queries;
using NetChallengeTestAPI.Services.Repositories;

namespace NetChallengeTestAPI.Handlers.ProductHandlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductsRepository _productsRepository;
        public GetProductByIdQueryHandler(IProductsRepository productsRepository)
            => _productsRepository = productsRepository;

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) 
            => await _productsRepository.FindByIdAsync(request.Id);
    }
}
