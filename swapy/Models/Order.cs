#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace swapy.Models;
public class Order
{
    [Key]
    public int OrderId { get; set; }
    [Required]
    public int BuyerId { get; set; }
    public User Buyer { get; set; } //! No Question Mark ? Self Join
    public int SellerId { get; set; }
    public User Seller { get; set; } //! No Question Mark ? Self Join
    [Required]
    public int ProductId {get;set;}
    public Product? Product {get;set;} //- Question Mark Because Foriegn Key
    [Required]
    public int Quantity {get;set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}

