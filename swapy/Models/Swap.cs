#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace swapy.Models;
public class Swap
{
    [Key]
    public int SwapId { get; set; }
    [Required]
    public int OwnId { get; set; }
    public Product Own { get; set; } //! No Question Mark ? Self Join
    public int SwapperId { get; set; }
    public Product swap { get; set; } //! No Question Mark ? Self Join
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}

