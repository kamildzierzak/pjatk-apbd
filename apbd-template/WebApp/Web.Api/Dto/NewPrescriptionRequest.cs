using System.ComponentModel.DataAnnotations;

namespace WebApp.Dto;

public class NewPrescriptionRequest
{
    [Required]
    public PatientDto Patient { get; set; }

    [Required]
    public List<MedicamentDto> Medicaments { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public int IdDoctor { get; set; }
}

public class PatientDto
{
    public int? IdPatient { get; set; }
    [Required, MaxLength(100)]
    public string FirstName { get; set; }
    [Required, MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    public DateTime Birthdate { get; set; }
}

public class MedicamentDto
{
    [Required]
    public int IdMedicament { get; set; }
    [Required]
    public int Dose { get; set; }
    [MaxLength(100)]
    public string Details { get; set; }
}

