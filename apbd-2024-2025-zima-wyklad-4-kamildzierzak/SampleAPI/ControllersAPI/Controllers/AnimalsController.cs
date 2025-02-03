using ControllersAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllersAPI.Controllers;

[Route("api/animals")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private static readonly List<Animal> _animals = new List<Animal>
{
    new Animal {IdAnimal = 1, Name = "Bobby", Category = AnimalCategory.CAT, Weight = 15, FurColor = AnimalFurColor.BLACK},
    new Animal {IdAnimal = 2, Name = "Marry", Category = AnimalCategory.DOG, Weight = 25.5, FurColor = AnimalFurColor.WHITE },
    new Animal {IdAnimal = 3, Name = "Koko", Category = AnimalCategory.COW, Weight = 999, FurColor= AnimalFurColor.BLACK },
    new Animal {IdAnimal = 4, Name = "Malala", Category = AnimalCategory.RAT, Weight = 0.3, FurColor = AnimalFurColor.GRAY}
};

    [HttpGet]
    public IActionResult GetAnimals()
    {
        return Ok(_animals);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimal(int id)
    {
        var animal = _animals.FirstOrDefault(a => a.IdAnimal == id);
        if (animal == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }

        return Ok(animal);
    }

    [HttpPost]
    public IActionResult CreateAnimal(Animal animal)
    {
        _animals.Add(animal);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, Animal animal)
    {
        var animalToEdit = _animals.FirstOrDefault(a => a.IdAnimal == id);

        if (animalToEdit == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }

        _animals.Remove(animalToEdit);
        _animals.Add(animal);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animalToEdit = _animals.FirstOrDefault(a => a.IdAnimal == id);
        if (animalToEdit == null)
        {
            return NoContent();
        }

        _animals.Remove(animalToEdit);
        return NoContent();
    }

}