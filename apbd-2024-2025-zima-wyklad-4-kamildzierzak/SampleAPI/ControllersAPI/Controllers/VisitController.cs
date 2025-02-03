using ControllersAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllersAPI.Controllers;

[Route("api/animals/{id}/visits")]
[ApiController]
public class VisitController : ControllerBase
{

    private static readonly List<Visit> _visits = new List<Visit>
    {
        new Visit {IdVisit = 1, DateOfVisit = new DateTime(2024, 11, 1), IdAnimal = 1, Description = "Cat keep staring at everyone and miauking.", Price = 330.0},
        new Visit {IdVisit = 2, DateOfVisit = new DateTime(2024, 11, 2), IdAnimal = 2, Description = "DogAteSpaceBarFromMyKeyboard.NeedToGetItOutFronHim.", Price = 230.0},
        new Visit {IdVisit = 3, DateOfVisit = new DateTime(2024, 11, 3), IdAnimal = 3, Description = "Cow doesn't moo enough.", Price = 5330.0},
        new Visit {IdVisit = 4, DateOfVisit = new DateTime(2024, 11, 4), IdAnimal = 4, Description = "Rat stopped liking cheese. We need to solve it!", Price = 30.0},
    };

    [HttpGet]
    public IActionResult GetVisits(int id)
    {
        var visits = _visits.Where(v => v.IdAnimal == id).ToList();
        return visits.Any() ? Ok(visits) : NotFound($"No visits found for animal with id {id}");
    }

    [HttpGet("{idVisit:int}")]
    public IActionResult GetVisit(int id, int idVisit)
    {
        var visit = _visits.FirstOrDefault(v => v.IdAnimal == id && v.IdVisit == idVisit);

        return visit != null ? Ok(visit) : NotFound($"Visit with id {idVisit} for animal with id {id} not found");
    }

    [HttpPost]
    public IActionResult CreateVisit(int id, Visit visit)
    {
        visit.IdAnimal = id;
        _visits.Add(visit);
        return StatusCode(StatusCodes.Status201Created);
    }
}
