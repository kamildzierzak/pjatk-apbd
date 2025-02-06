using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebApp.Models;

public class PrescriptionMedicament
{
    [Key, Column(Order = 1)]
    [ForeignKey("Medicament")]
    public int IdMedicament { get; set; }
    public Medicament Medicament { get; set; }

    [Key, Column(Order = 2)]
    [ForeignKey("Prescription")]
    public int IdPrescription { get; set; }
    public Prescription Prescription { get; set; }

    public int Dose { get; set; }

    [MaxLength(100)]
    public string Details { get; set; }
}
