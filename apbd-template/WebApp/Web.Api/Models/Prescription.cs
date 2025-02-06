using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }

    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    [ForeignKey("Patient")]
    public int IdPatient { get; set; }
    public Patient Patient { get; set; }

    [ForeignKey("Doctor")]
    public int IdDoctor { get; set; }
    public Doctor Doctor { get; set; }

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
}
