using System.ComponentModel.DataAnnotations;

namespace Exercise8.DTO;

public class DoctorDto
{
    public int? IdDoctor { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Email { get; set; }

}