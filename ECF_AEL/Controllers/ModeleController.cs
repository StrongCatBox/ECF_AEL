using ECF_AEL.Metiers;
using ECF_AEL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ECF_AEL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelesController : ControllerBase
    {
        private readonly ModeleMetier _metier;

        public ModelesController(IConfiguration config)
        {
            var cs = config.GetConnectionString("AELdb");
            _metier = new ModeleMetier(cs);
        }

        [HttpGet("liste")]
        public ActionResult<IEnumerable<ModeleVehicule>> ListerModeles()
        {
            try
            {
                var modeles = _metier.ListerModeles();
                return Ok(modeles);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la récupération des modèles : {ex.Message}");
            }
        }

        // POST: api/modeles/ajouter


        [HttpPost("ajouter")]
        public IActionResult AjouterModele([FromBody] ModeleVehicule modeleVehicule)
        {
            try
            {
                _metier.AjouterModele(modeleVehicule);
                return Ok("Modele ajouté avec succès");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

    }
}
