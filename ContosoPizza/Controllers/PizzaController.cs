using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
        PizzaService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id) =>
    PizzaService.Get(id) is Pizza pizza ? Ok(pizza) : NotFound();

    // POST action
    [HttpPost]
    public ActionResult<Pizza> Post(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Post), new { id = pizza.Id }, pizza);
    }

    // PUT action
    [HttpPut("{id}")]
    public ActionResult<Pizza> Put(int id, Pizza pizza)
    {
        PizzaService.Update(pizza);
        return Ok(pizza);
    }

    // DELETE action
    [HttpDelete("{id}")]
    public ActionResult<Pizza> Delete(int id)
    {
        PizzaService.Delete(id);
        return Ok();
    }
}