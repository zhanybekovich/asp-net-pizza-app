using CondozoPizza.Models;
using Marten;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CondozoPizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IDocumentStore _documentStore;

        public PizzaController(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePizza([FromBody] Pizza pizza)
        {
            try
            {
                using var session = _documentStore.LightweightSession();

                // Assign a unique ID to the pizza
                pizza.Id = Guid.NewGuid();

                // Save the pizza to the database
                session.Store(pizza);
                await session.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPizzaById), new { id = pizza.Id }, pizza);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating pizza: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPizzaById(Guid id)
        {
            try
            {
                using var session = _documentStore.QuerySession();
                var pizza = await session.LoadAsync<Pizza>(id);

                if (pizza == null)
                    return NotFound();

                return Ok(pizza);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving pizza: {ex.Message}");
            }
        }
    }
}
