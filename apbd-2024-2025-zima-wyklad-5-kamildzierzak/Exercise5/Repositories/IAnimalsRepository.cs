using Exercise5.Model;

namespace Exercise5.Repositories
{
    public interface IAnimalsRepository
    {
        int CreateAnimal(Animal animal);
        Animal GetAnimal(long animalId);
        IEnumerable<Animal> GetAnimals(string orderBy);
        int UpdateAnimal(long id, Animal animal);
        int DeleteAnimal(long animalId);
    }
}
