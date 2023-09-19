using MediatR;
using NetChallengeTest.Core.Models;

namespace NetChallengeTest.Core.Queries
{
    public class GetProductByProductCodeQuery : IRequest<Product>
    {
        public string ProductCode { get; set; }
    }
}