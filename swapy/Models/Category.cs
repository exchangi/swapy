#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace swapy.Models;
public class Category
{
    [Key]
    public int CategoryId { get; set; } 
    [Required]
    [MinLength(2, ErrorMessage="Name must be 3 characters or longer!")]
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public List<Association> CategoryProducts {get;set;} = new List<Association>();
}

