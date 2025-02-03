using System.ComponentModel.DataAnnotations;

namespace Exercise6.Model;

public class Order
{
    [Key]
    public int IdOrder { get; set; }
    public int IdProduct { get; set; }

    [Required(ErrorMessage = "Amount is required.")]
    public int Amount { get; set; }

    [Required(ErrorMessage = "CreatedAt is required.")]
    public DateTime CreatedAt { get; set; }

    public DateTime? FulfilledAt { get; set; }

    public ICollection<ProductWarehouse> ProductWarehouses { get; set; }
}
