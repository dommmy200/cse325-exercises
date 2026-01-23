namespace BlazingPizza.Services
{
    public class PizzaService
    {
        // This method is what your component calls in OnInitializedAsync
        public Task<List<BlazingPizza.Data.Pizza>> GetPizzasAsync()
        {
            // Call your data access technology here
            return Task.FromResult(new List<BlazingPizza.Data.Pizza> { });
        }
    }
}