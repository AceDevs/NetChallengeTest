using MediatR;
using NetChallengeTest.Core.Models;

namespace NetChallengeTest.Core.Queries;

public class GetCategoryByIdQuery : IRequest<Category>
{
    public int Id { get; set; }
}