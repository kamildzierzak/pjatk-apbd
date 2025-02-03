using System.ComponentModel.DataAnnotations;

namespace Exercise6.Model;

public class Product
{
    [Key]
    public int IdProduct { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(200, ErrorMessage = "Name cannot be longer than 200 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
    public string Description { get; set; }

    public decimal Price { get; set; }

    public ICollection<ProductWarehouse> ProductWarehouses { get; set; }
}
