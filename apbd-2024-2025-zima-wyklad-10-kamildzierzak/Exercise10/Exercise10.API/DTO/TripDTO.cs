namespace Exercise10.API.DTO;

public class TripDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public IList<CountryDto> Countries { get; set; }
    public IList<ClientDto> Clients { get; set; }
}
