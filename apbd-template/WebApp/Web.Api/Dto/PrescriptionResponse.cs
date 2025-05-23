﻿using WebApp.Dto;

namespace Web.Api.Dto;

public class PrescriptionResponse
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorDto Doctor { get; set; }
    public PatientDto Patient { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
}