using MediatR;
using NetChallengeTest.Core.Models;
using NetChallengeTest.Core.Queries;
using NetChallengeTestAPI.Services.Repositories;

namespace NetChallengeTestAPI.Handlers.CategoryHandlers
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IList<Category>>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public GetCategoriesQueryHandler(ICategoriesRepository categoriesRepository) 
            => _categoriesRepository = categoriesRepository;

        public async Task<IList<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken) 
            => await _categoriesRepository.GetAsync(request.Page, request.PageSize, request.SortBy);
    }
}
