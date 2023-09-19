using MediatR;
using NetChallengeTest.Core.Models;

namespace NetChallengeTest.Core.Queries
{
    public class GetProductByNameQuery : IRequest<Product>
    {
        public string Name { get; set; }
    }
}