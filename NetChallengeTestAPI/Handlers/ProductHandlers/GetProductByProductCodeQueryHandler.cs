using MediatR;
using NetChallengeTest.Core.Models;
using NetChallengeTest.Core.Queries;
using NetChallengeTestAPI.Services.Repositories;

namespace NetChallengeTestAPI.Handlers.ProductHandlers
{
    namespace NetChallengeTestAPI.Handlers.ProductHandlers
    {
        public class GetProductByProductCodeQueryHandler : IRequestHandler<GetProductByProductCodeQuery, Product>
        {
            private readonly IProductsRepository _productsRepository;
            public GetProductByProductCodeQueryHandler(IProductsRepository productsRepository)
                => _productsRepository = productsRepository;

            public async Task<Product> Handle(GetProductByProductCodeQuery request, CancellationToken cancellationToken) 
                => await _productsRepository.FindByCodeAsync(request.ProductCode);
        }
    }

}
