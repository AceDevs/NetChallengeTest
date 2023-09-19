using System.ComponentModel.DataAnnotations;

namespace NetChallengeTest.Core.Models;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string? ProductName { get; set; }
    [Required]
    public string? ProductCode { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public DateTime CreationDate { get; set; }
}
