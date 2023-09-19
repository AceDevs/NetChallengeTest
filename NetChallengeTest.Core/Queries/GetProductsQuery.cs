using MediatR;
using NetChallengeTest.Core.Models;

namespace NetChallengeTest.Core.Queries
{
    public class GetProductsQuery : IRequest<IList<Product>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
    }
}