using MediatR;
using NetChallengeTest.Core.Models;
using NetChallengeTest.Core.Queries;
using NetChallengeTestAPI.Services.Repositories;

namespace NetChallengeTestAPI.Handlers.ProductHandlers
{
    namespace NetChallengeTestAPI.Handlers.ProductHandlers
    {
        public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, Product>
        {
            private readonly IProductsRepository _productsRepository;
            public GetProductByNameQueryHandler(IProductsRepository productsRepository)
                => _productsRepository = productsRepository;

            public async Task<Product> Handle(GetProductByNameQuery request, CancellationToken cancellationToken) 
                => await _productsRepository.FindByNameAsync(request.Name);
        }
    }

}
