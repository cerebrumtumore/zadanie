using System;
List<Cosmetic> cosm = new List<Cosmetic>();

Repository repo = new Repository();

var builder = WebApplication.CreateBuilder(args);

// ... (rest of your setup)

var app = builder.Build();

// ... (rest of your setup)

app.MapGet("/cosmetic/{id}", (Guid id) => repo.Read(id));
app.MapPost("/add", (Cosmetic c) => repo.Add(c));
app.MapDelete("/delete/{id}", (Guid id) => repo.Delete(id));
app.MapPut("/update/{id}", (Guid id, Cosmetic update) => repo.Update(id, update));
app.MapGet("/getAll", () => repo.readAll()); // Call readAll() directly

// ... (rest of your setup)

app.Run();



class Cosmetic
{
    public Guid Id { get; set; }
    private string name;
    private int articul;
    private string description;
    private int price;

    public Cosmetic(string name, int articul, string description, int price)
    {
        Id = Guid.NewGuid();
        this.name = name;
        this.articul = articul;
        this.description = description;
        this.price = price;
    }

    public string Name { get => name; set => name = value; }
    public int Articul { get => articul; set => articul = value; }
    public string Description { get => description; set => description = value; }
    public int Price { get => price; set => price = value; }


    public void Update(Cosmetic updatedCosmetic)
    {
        if (updatedCosmetic != null)
        {
            this.Name = updatedCosmetic.Name;
            this.Articul = updatedCosmetic.Articul;
            this.Description = updatedCosmetic.Description;
            this.Price = updatedCosmetic.Price;
        }
    }

}


class Repository
{
    public List<Cosmetic> repo { get; set; }

    public Repository()
    {
        repo = new List<Cosmetic>();
    }

    public void Add(Cosmetic cosmetic)
    {
        repo.Add(cosmetic);
    }
    public Cosmetic Read(Guid id)
    {
        return repo.Find(item => item.Id == id);
    }


    public List<Cosmetic> readAll()
    {
        return repo;
    }

    public void Delete(Guid id)
    {
        repo.Remove(Read(id));
    }
    public void Update(Guid id, Cosmetic updatedCosmetic)
    {
        if (updatedCosmetic != null)
        {
            var cosmeticToUpdate = repo.Find(item => item.Id == id);
            if (cosmeticToUpdate != null)
            {
                cosmeticToUpdate.Update(updatedCosmetic);
            }
        }
    }


}
