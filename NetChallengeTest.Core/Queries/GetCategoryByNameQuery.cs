using MediatR;
using NetChallengeTest.Core.Models;

namespace NetChallengeTest.Core.Queries;

public class GetCategoryByNameQuery : IRequest<Category>
{
    public string Name { get; set; }
}