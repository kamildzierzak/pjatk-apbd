using System.ComponentModel.DataAnnotations;

namespace Exercise6.Model;

public class Warehouse
{
    [Key]
    public int IdWarehouse { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(200, ErrorMessage = "Name cannot be longer than 200 characters.")]
    public string Name;

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
    public string Address;

    public ICollection<ProductWarehouse> ProductWarehouses { get; set; }
}
