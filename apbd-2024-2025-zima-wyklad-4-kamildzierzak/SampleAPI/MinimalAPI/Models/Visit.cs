namespace MinimalAPI.Models;
public class Visit
{
    public int IdVisit { get; set; }
    public DateTime DateOfVisit { get; set; }
    public int IdAnimal { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }

}
