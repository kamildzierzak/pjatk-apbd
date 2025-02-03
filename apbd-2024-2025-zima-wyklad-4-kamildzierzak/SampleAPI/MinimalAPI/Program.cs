using MinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


var _animals = new List<Animal>
{
    new Animal {IdAnimal = 1, Name = "John", Category = AnimalCategory.CAT, Weight = 15, FurColor = AnimalFurColor.BLACK},
    new Animal {IdAnimal = 2, Name = "Larry", Category = AnimalCategory.DOG, Weight = 25.5, FurColor = AnimalFurColor.WHITE },
    new Animal {IdAnimal = 3, Name = "Sara", Category = AnimalCategory.COW, Weight = 999, FurColor= AnimalFurColor.BLACK },
    new Animal {IdAnimal = 4, Name = "Rat", Category = AnimalCategory.RAT, Weight = 0.3, FurColor = AnimalFurColor.GRAY}
};

var _visits = new List<Visit>
    {
        new Visit {IdVisit = 1, DateOfVisit = new DateTime(2024, 11, 1), IdAnimal = 1, Description = "Cat keep staring at everyone and miauking.", Price = 330.0},
        new Visit {IdVisit = 2, DateOfVisit = new DateTime(2024, 11, 2), IdAnimal = 2, Description = "DogAteSpaceBarFromMyKeyboard.NeedToGetItOutFronHim.", Price = 230.0},
        new Visit {IdVisit = 3, DateOfVisit = new DateTime(2024, 11, 3), IdAnimal = 3, Description = "Cow doesn't moo enough.", Price = 5330.0},
    };

app.MapGet("/api/animals", () => Results.Ok(_animals))
    .WithName("GetAnimals")
    .WithOpenApi();

app.MapGet("/api/animals/{id:int}", (int id) =>
    {
        var animal = _animals.FirstOrDefault(a => a.IdAnimal == id);
        return animal == null ? Results.NotFound($"Animal with id {id} was not found") : Results.Ok(animal);
    })
    .WithName("GetAnimal")
    .WithOpenApi();

app.MapPost("/api/animals", (Animal animal) =>
    {
        _animals.Add(animal);
        return Results.StatusCode(StatusCodes.Status201Created);
    })
    .WithName("CreateAnimal")
    .WithOpenApi();

app.MapPut("/api/animals/{id:int}", (int id, Animal animal) =>
    {
        var animalToEdit = _animals.FirstOrDefault(a => a.IdAnimal == id);
        if (animalToEdit == null)
        {
            return Results.NotFound($"Animal with id {id} was not found");
        }
        _animals.Remove(animalToEdit);
        _animals.Add(animal);
        return Results.NoContent();
    })
    .WithName("UpdateAnimal")
    .WithOpenApi();

app.MapDelete("/api/animals/{id:int}", (int id) =>
    {
        var animalToDelete = _animals.FirstOrDefault(a => a.IdAnimal == id);
        if (animalToDelete == null)
        {
            return Results.NoContent();
        }
        _animals.Remove(animalToDelete);
        return Results.NoContent();
    })
    .WithName("DeleteAnimal")
    .WithOpenApi();

app.MapGet("/api/animals/{id}/visits", (int id) =>
{
    var animalVisits = _visits.Where(v => v.IdAnimal == id).ToList();
    return animalVisits.Any() ? Results.Ok(animalVisits) : Results.NotFound($"No visits found for animal with id {id}");
})
    .WithName("GetVisits")
    .WithOpenApi();

app.MapGet("/api/animals/{id}/visits/{visitId}", (int id, int idVisit) =>
{
    var visit = _visits.FirstOrDefault(v => v.IdAnimal == id && v.IdVisit == idVisit);
    return visit != null ? Results.Ok(visit) : Results.NotFound($"Visit with id {idVisit} for animal with id {id} not found");
})
    .WithName("GetVisit")
    .WithOpenApi();

app.MapPost("/api/animals/{id}/visits", (int id, Visit visit) =>
{
    visit.IdAnimal = id;
    _visits.Add(visit);

    return Results.StatusCode(StatusCodes.Status201Created);
})
    .WithName("CreateVisits")
    .WithOpenApi();

app.Run();
