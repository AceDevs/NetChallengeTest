using System.ComponentModel.DataAnnotations;

namespace NetChallengeTest.Core.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    public string? CategoryName { get; set; }
    [Required]
    public int CategoryCode { get; set; }
    public ICollection<Product>? Products { get; set; }
    public DateTime CreationDate { get; set; }
}