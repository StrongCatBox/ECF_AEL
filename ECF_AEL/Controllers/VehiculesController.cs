using Microsoft.AspNetCore.Mvc;
using ECF_AEL.Models;
using ECF_AEL.Repositories;
using ECF_AEL.Metier;

namespace ECF_AEL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculesController : ControllerBase
    {
        private readonly VehiculeMetier _metier;

        public VehiculesController(IConfiguration config)
        {
            var cs = config.GetConnectionString("AELdb");
            _metier = new VehiculeMetier(cs);
        }

        // POST: api/vehicules/ajouter


        [HttpPost("ajouter")]
        public IActionResult AjouterVehicule([FromBody] Vehicule vehicule)
        {
            try
            {
                _metier.AjouterVehicule(vehicule);
                return Ok("Véhicule ajouté avec succès");
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


        // GET: api/vehicules/liste


        [HttpGet("liste")]
        public ActionResult<IEnumerable<Vehicule>> ListerVehicules()
        {
            try
            {
                var vehicules = _metier.ListerVehicules();
                return Ok(vehicules);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la récupération des véhicules : {ex.Message}");
            }
        }

        // PUT: api/vehicules/update-etat/{immatric}

        [HttpPut("update-etat/{immatric}")]

        public IActionResult MettreAJourEtatVehic(string immatric, [FromBody] bool nouvelEtat)
        {
            try
            {
                _metier.MettreAJourEtat(immatric, nouvelEtat);
                return Ok($"Etat du vehicule {immatric} mise a jour :  {nouvelEtat}");
            }
            catch (Exception ex) {
                return BadRequest($"Erreur lors de la mise a jour de l'etat {ex.Message}");

            }
        }

        // DELETE: api/vehicules/{immatric}
        [HttpDelete("{immatric}")]
        public IActionResult SupprimerVehicule(string immatric)
        {
            try
            {
                _metier.SupprimerVehicule(immatric);
                return Ok($"Véhicule {immatric} supprimé avec succès.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la suppression du véhicule : {ex.Message}");
            }
        }

    }
}
