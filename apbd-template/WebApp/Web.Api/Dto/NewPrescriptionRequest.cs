using System.ComponentModel.DataAnnotations;

namespace WebApp.Dto;

public class NewPrescriptionRequest
{
    [Required]
    public NewPatientDto Patient { get; set; } // ✅ Renamed to avoid conflict

    [Required]
    public List<NewMedicamentDto> Medicaments { get; set; } // ✅ Renamed to avoid conflict

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public int IdDoctor { get; set; }
}

public class NewPatientDto // ✅ Renamed to NewPatientDto
{
    public int? IdPatient { get; set; }
    [Required, MaxLength(100)]
    public string FirstName { get; set; }
    [Required, MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    public DateTime Birthdate { get; set; }
}

public class NewMedicamentDto // ✅ Renamed to NewMedicamentDto
{
    [Required]
    public int IdMedicament { get; set; }
    [Required]
    public int Dose { get; set; }
    [MaxLength(100)]
    public string Details { get; set; }
}
