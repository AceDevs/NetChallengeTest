using MediatR;
using NetChallengeTest.Core.Models;

namespace NetChallengeTest.Core.Commands;

public class CreateCategoryCommand : IRequest<Category>
{
    public int CategoryCode { get; set; }
    public string CategoryName { get; set; }
}