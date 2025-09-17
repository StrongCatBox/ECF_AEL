using Microsoft.AspNetCore.Mvc;
using ECF_AEL.Repositories;

namespace ECF_AEL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendrierController : ControllerBase
    {
        private readonly CalendrierRepository _repo;

        public CalendrierController(IConfiguration config)
        {
            var cs = config.GetConnectionString("AELdb");
            _repo = new CalendrierRepository(cs);
        }

        [HttpGet("liste")]
        public ActionResult<IEnumerable<DateTime>> ListerCreneaux()
        {
            return Ok(_repo.ListerCreneaux());
        }

        [HttpPost("ajouter")]
        public IActionResult AjouterCreneau([FromBody] DateTime dateHeure)
        {
            if (dateHeure < DateTime.Now)
                return BadRequest("Impossible d’ajouter un créneau dans le passé.");

            _repo.AjouterCreneau(dateHeure);
            return Ok("Créneau ajouté avec succès.");
        }

        [HttpDelete("supprimer")]
        public IActionResult SupprimerCreneau([FromBody] DateTime dateHeure)
        {
            _repo.SupprimerCreneau(dateHeure);
            return Ok("Créneau supprimé avec succès.");
        }
    }
}
