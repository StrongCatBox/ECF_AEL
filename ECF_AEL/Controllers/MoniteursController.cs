using ECF_AEL.Models;
using ECF_AEL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECF_AEL.Metier;

namespace ECF_AEL.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MoniteursController : ControllerBase
    {
        private readonly MoniteurMetier _metier;

        public MoniteursController(IConfiguration config)
        {
            var cs = config.GetConnectionString("AELdb");
            _metier = new MoniteurMetier(cs);
        }
        // POST api/moniteurs/ajouter

        [HttpPost("ajouter")]
        public IActionResult AjouterMoniteur([FromBody] Moniteur moniteur)
        {
            try
            {
                _metier.AjouterMoniteur(moniteur);
                return Ok("Moniteur ajouté avec succès");
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

        // GET: api/moniteurs/liste
        [HttpGet("liste")]
        public ActionResult<IEnumerable<Moniteur>> ListerMoniteurs()
        {
            try
            {
                var moniteurs = _metier.ListerMoniteurs();
                return Ok(moniteurs);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la récupération des moniteurs : {ex.Message}");
            }
        }

        // PUT api/moniteurs/update-activite/{id}
        [HttpPut("update-activite/{id}")]
        public IActionResult UpdateActivite(int id, [FromBody] bool nouvelleActivite)
        {
            try
            {
                _metier.MettreAJourActivite(id, nouvelleActivite);
                return Ok($"Activité du moniteur {id} mise à jour : {nouvelleActivite}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la mise à jour de l'activité : {ex.Message}");
            }
        }


        // DELETE api/moniteurs/{id}
        [HttpDelete("{id}")]
        public IActionResult SupprimerMoniteur(int id)
        {
            try
            {
                _metier.SupprimerMoniteur(id);
                return Ok($"Moniteur {id} supprimé avec succès.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la suppression du moniteur : {ex.Message}");
            }
        }

    }
}
