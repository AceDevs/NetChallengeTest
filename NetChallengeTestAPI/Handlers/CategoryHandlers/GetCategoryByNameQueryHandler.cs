using MediatR;
using NetChallengeTest.Core.Models;
using NetChallengeTest.Core.Queries;
using NetChallengeTestAPI.Services.Repositories;

namespace NetChallengeTestAPI.Handlers.CategoryHandlers
{
    namespace NetChallengeTestAPI.Handlers.CategoryHandlers
    {
        public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, Category>
        {
            private readonly ICategoriesRepository _categoriesRepository;
            public GetCategoryByNameQueryHandler(ICategoriesRepository categoriesRepository)
                => _categoriesRepository = categoriesRepository;

            public async Task<Category> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken) 
                => await _categoriesRepository.FindByNameAsync(request.Name);
        }
    }

}
