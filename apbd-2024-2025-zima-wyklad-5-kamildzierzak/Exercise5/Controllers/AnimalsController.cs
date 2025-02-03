using Exercise5.Model;
using Exercise5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exercise5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {

        private IAnimalsService _animalsService;

        public AnimalsController(IAnimalsService animalsService)
        {
            _animalsService = animalsService;
        }

        /// <summary>
        /// Endpoint used to create an animal.
        /// </summary>
        /// <param name="animal">New animal data</param>
        /// <returns>201 Created</returns>
        [HttpPost]
        public IActionResult CreateAnimal(Animal animal)
        {
            if (string.IsNullOrEmpty(animal.Name) || string.IsNullOrEmpty(animal.Category) || string.IsNullOrEmpty(animal.Area))
            {
                return BadRequest("Name, Category, and Area are required fields.");
            }

            var affectedCount = _animalsService.CreateAnimal(animal);

            if (affectedCount == 0)
            {
                return BadRequest("Couldn't create the animal");
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Endpoint used to return a single animal.
        /// </summary>
        /// <param name="id">Id of an animal</param>
        /// <returns>Animal</returns>
        [HttpGet("{id:long}")]
        public IActionResult GetAnimal(long id)
        {
            var animal = _animalsService.GetAnimal(id);

            if (animal == null)
            {
                return NotFound($"Animal with id {id} not found");
            }

            return Ok(animal);
        }

        /// <summary>
        /// Endpoints used to return list of animals.
        /// </summary>
        /// <returns>List of animals</returns>
        [HttpGet]
        public IActionResult GetAnimals([FromQuery] string orderBy = "name")
        {
            try
            {
                var animals = _animalsService.GetAnimals(orderBy);

                return Ok(animals);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint used to update an animal.
        /// </summary>
        /// <param name="id">Id of an animal</param>
        /// <param name="animal">204 No Content</param>
        /// <returns></returns>
        [HttpPut("{id:long}")]
        public IActionResult UpdateAnimal(long id, [FromBody] Animal animal)
        {
            try
            {
                var affectedCount = _animalsService.UpdateAnimal(id, animal);

                if (affectedCount == 0)
                {
                    return NotFound($"Animal with id {id} not found.");
                }

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint used to delete an animal.
        /// </summary>
        /// <param name="id">Id of an animal</param>
        /// <returns>204 No Content</returns>
        [HttpDelete("{id:long}")]
        public IActionResult DeleteAnimal(long id)
        {
            var affectedCount = _animalsService.DeleteAnimal(id);

            if (affectedCount == 0)
            {
                return NotFound($"Animal with id {id} not found.");
            }

            return NoContent();
        }
    }
}
