using System.ComponentModel.DataAnnotations;

namespace Exercise8.DTO;

public class PrescriptionDto
{
    public int IdPrescription { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    public IEnumerable<MedicamentDto> Medicaments { get; set; }

    public DoctorDto Doctor { get; set; }
}