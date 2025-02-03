using System.ComponentModel.DataAnnotations;

namespace Exercise8.DTO;

public class PatientDto
{
    public int? IdPatient { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    public DateTime Birthdate { get; set; }

    public IEnumerable<PrescriptionDto> Prescriptions { get; set; }

}