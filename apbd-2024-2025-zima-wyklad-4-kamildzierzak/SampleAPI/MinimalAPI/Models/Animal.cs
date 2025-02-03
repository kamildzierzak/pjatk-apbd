namespace MinimalAPI.Models;

public class Animal
{
    public int IdAnimal { get; set; }
    public string Name { get; set; }
    public AnimalCategory Category { get; set; }
    public double Weight { get; set; }
    public AnimalFurColor FurColor { get; set; }


}
