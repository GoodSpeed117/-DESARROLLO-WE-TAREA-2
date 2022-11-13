using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApiAviones.Entidades;

namespace WebApiAviones.Controllers
{

    [ApiController]
    [Route("api/aviones")]
    public class AvionesController : ControllerBase
    {
        private readonly AplicationDbContext dbContext;

        public AvionesController(AplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Avion>>> Get()
        {
            return await dbContext.Aviones.ToListAsync();
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Avion>> PrimerAvion([FromHeader]int valor, [FromQuery] string avion, [FromForm]int avionid)
        {
            return await dbContext.Aviones.FirstOrDefaultAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Avion>> Get(int id) 
        {
            var avion = await dbContext.Aviones.FirstOrDefaultAsync(a => a.Id == id);
            if (avion == null)
            {
                return NotFound();
            }
            return avion;
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<Avion>> Get([FromRoute] string name)
        {
            var avion = await dbContext.Aviones.FirstOrDefaultAsync(a => a.Name.Contains(name));
            if (avion == null)
            {
                return NotFound();
            }
            return avion;
        }


        [HttpPost]
        public async Task<ActionResult> Post(Avion avion)
        { 
            dbContext.Add(avion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(Avion avion, int id)
        {
            if(avion.Id != id)
            {
                return BadRequest("El id del avion no coincide con el establecido en la url");
            }

            dbContext.Update(avion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Aviones.AnyAsync(x => x.Id == id);
            if(!exist)
            {
                return NotFound("El recuerso no fue encontrado");
            }

            dbContext.Remove(new Avion { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
