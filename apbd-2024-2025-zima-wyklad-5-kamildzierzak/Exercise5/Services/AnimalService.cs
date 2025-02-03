using Exercise5.Model;
using Exercise5.Repositories;

namespace Exercise5.Services
{
    public class AnimalService : IAnimalsService
    {
        private readonly IAnimalsRepository _animalsRepository;

        public AnimalService(IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }

        public int CreateAnimal(Animal animal)
        {
            return _animalsRepository.CreateAnimal(animal);
        }

        public Animal GetAnimal(long animalId)
        {
            return _animalsRepository.GetAnimal(animalId);
        }

        public IEnumerable<Animal> GetAnimals(string orderBy)
        {
            return _animalsRepository.GetAnimals(orderBy);
        }

        public int UpdateAnimal(long id, Animal animal)
        {
            if (id != animal.AnimalId)
            {
                throw new ArgumentException("The AnimalId in the path and body don't match.");
            }

            return _animalsRepository.UpdateAnimal(id, animal);
        }

        public int DeleteAnimal(long animalId)
        {
            return _animalsRepository.DeleteAnimal(animalId);
        }
    }
}
