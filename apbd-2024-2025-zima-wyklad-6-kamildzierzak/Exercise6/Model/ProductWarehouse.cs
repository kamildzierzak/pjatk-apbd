using System.ComponentModel.DataAnnotations;

namespace Exercise6.Model;

public class ProductWarehouse
{
    [Key]
    public int IdProductWarehouse { get; set; }

    [Required]
    public int IdWarehouse { get; set; }
    public Warehouse Warehouse { get; set; }

    [Required]
    public int IdProduct { get; set; }
    public Product Product { get; set; }

    [Required]
    public string IdOrder { get; set; }
    public Order Order { get; set; }

    [Required]
    public int amount { get; set; }

    [Required]
    public decimal price { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

}