using Exercise5.Model;
using Microsoft.Data.Sqlite;

namespace Exercise5.Repositories
{
    public class AnimalsRepository : IAnimalsRepository
    {
        private IConfiguration _configuration;

        public AnimalsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int CreateAnimal(Animal animal)
        {
            using var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            using var command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO Animal(Name, Description, Category, Area) VALUES(@Name, @Description, @Category, @Area)";

            //command.Parameters.AddWithValue("@AnimalId", animal.AnimalId);
            command.Parameters.AddWithValue("@Name", animal.Name);
            command.Parameters.AddWithValue("@Description", (object)animal.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@Category", animal.Category);
            command.Parameters.AddWithValue("@Area", animal.Area);

            var affectedCount = command.ExecuteNonQuery();
            return affectedCount;

        }

        public Animal GetAnimal(long animalId)
        {
            using var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            using var command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "SELECT AnimalId, Name, Description, Category, Area FROM Animal WHERE AnimalId = @AnimalId";
            command.Parameters.AddWithValue("@AnimalId", animalId);

            var dr = command.ExecuteReader();

            if (!dr.Read()) return null;

            var animal = new Animal
            {
                AnimalId = (long)dr["AnimalId"],
                Name = dr["Name"].ToString(),
                Description = dr["Description"].ToString(),
                Category = dr["Category"].ToString(),
                Area = dr["Area"].ToString(),
            };

            return animal;
        }

        public IEnumerable<Animal> GetAnimals(string orderBy = "name")
        {
            var validColumns = new List<String> { "name", "description", "category", "area" };
            if (!validColumns.Contains(orderBy.ToLower()))
            {
                throw new ArgumentException($"Invalid orderBy parameter: {orderBy}. Allowed values: {string.Join(", ", validColumns)}");
            }

            using var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            using var command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT AnimalId, Name, Description, Category, Area FROM Animal ORDER BY {orderBy}";

            var dr = command.ExecuteReader();
            var animals = new List<Animal>();

            while (dr.Read())
            {

                var animal = new Animal
                {
                    AnimalId = (long)dr["AnimalId"],
                    Name = dr["Name"].ToString(),
                    Description = dr["Description"].ToString(),
                    Category = dr["Category"].ToString(),
                    Area = dr["Area"].ToString(),
                };
                animals.Add(animal);
            }

            return animals;
        }

        public int UpdateAnimal(long id, Animal animal)
        {
            using var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            using var command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE Animal SET Name=@Name, Description=@Description, Category=@Category, Area=@Area WHERE AnimalId = @AnimalId";
            command.Parameters.AddWithValue("@AnimalId", id);
            command.Parameters.AddWithValue("Name", animal.Name);
            command.Parameters.AddWithValue("Description", animal.Description);
            command.Parameters.AddWithValue("Category", animal.Category);
            command.Parameters.AddWithValue("Area", animal.Area);

            var affectedCount = command.ExecuteNonQuery();
            return affectedCount;
        }

        public int DeleteAnimal(long animalId)
        {
            using var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            using var command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM Animal WHERE AnimalId = @AnimalId";
            command.Parameters.AddWithValue("@AnimalId", animalId);

            var affectedCount = command.ExecuteNonQuery();
            return affectedCount;
        }

    }
}
