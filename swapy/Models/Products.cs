#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace swapy.Models;
public class Product
{
    [Key]
    public int ProductId { get; set; } 
    [Required]
    [MinLength(2, ErrorMessage="¨Product Name must be 3 characters or longer!")]
    public string Name { get; set; }
    [Required]
    [MinLength(10, ErrorMessage="Product Description must be 10 characters or longer!")]
    public string Description { get; set; }
    [Required]
    [Range(0.0,999999.9)]
    public double Price {get; set;}
    [Range(1,99999)]
    public int Quantity { get; set; } 
    public int UserId {get;set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public User? User {get;set;}
    public List<Association> ProductCategories {get;set;} = new List<Association>();
    public List<Order> ProductOrdered {get;set;} = new List<Order>();
    [InverseProperty("swap")]
    public List<Swap> Swapper { get; set; } = new List<Swap>();
    [InverseProperty("Own")]
    public List<Swap> Owner { get; set; } = new List<Swap>();

}

