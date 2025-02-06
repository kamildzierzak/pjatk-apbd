namespace Web.Api.Dto;

public class PatientDetailsResponse
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<PrescriptionResponse> Prescriptions { get; set; }
}