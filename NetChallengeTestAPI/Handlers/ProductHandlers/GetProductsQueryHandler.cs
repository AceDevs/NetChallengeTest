using MediatR;
using NetChallengeTest.Core.Models;
using NetChallengeTest.Core.Queries;
using NetChallengeTestAPI.Services.Repositories;

namespace NetChallengeTestAPI.Handlers.ProductHandlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IList<Product>>
    {
        private readonly IProductsRepository _productsRepository;
        public GetProductsQueryHandler(IProductsRepository productsRepository) 
            => _productsRepository = productsRepository;

        public async Task<IList<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken) 
            => await _productsRepository.GetAsync(request.Page, request.PageSize, request.SortBy);
    }
}
