namespace BlazingPizza.Data;

public class Pizza
{
    public int PizzaId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool Vegetarian { get; set; }

    public bool Vegan { get; set; }
}

public class PizzaService
{
    public Task<Pizza[]> GetPizzasAsync()
    {
        // Call your data access technology here
        return Task.FromResult(new Pizza[] { });
    }
}