using CondozoPizza.Models;
using CondozoPizza.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CondozoPizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() =>
            PizzaService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return pizza;
        }
    }
}
