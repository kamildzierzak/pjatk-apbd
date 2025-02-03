using System.ComponentModel.DataAnnotations;

namespace Exercise8.DTO;

public class MedicamentDto
{
    public int IdMedicament { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public int? Dose { get; set; }

    [Required]
    [MaxLength(100)]
    public string Description { get; set; }

}