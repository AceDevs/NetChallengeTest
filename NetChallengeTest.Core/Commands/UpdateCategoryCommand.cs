using MediatR;
using NetChallengeTest.Core.Models;

namespace NetChallengeTest.Core.Commands;

public class UpdateCategoryCommand : IRequest<Category>
{
    public int Id { get; set; }
    public int CategoryCode { get; set; }
    public string CategoryName { get; set; }
}