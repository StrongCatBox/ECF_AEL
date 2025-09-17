using Microsoft.AspNetCore.Mvc;
using ECF_AEL.Models;
using ECF_AEL.Metier;

namespace ECF_AEL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElevesController : ControllerBase
    {
        private readonly EleveMetier _metier;

        public ElevesController(IConfiguration config)
        {
            var cs = config.GetConnectionString("AELdb");
            _metier = new EleveMetier(cs); 
        }


        // POST api/eleves/ajouter

        [HttpPost("ajouter")]
        public IActionResult AjouterEleve([FromBody] Eleve eleve)
        { 

         

            try
            {
                _metier.AjouterUnEleve(eleve);
                return Ok("Eleve ajouté avec success");
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

        // GET api/eleves/liste

        [HttpGet("liste")]

        public ActionResult<IEnumerable<Eleve>> ListerEleves()
        {


            try
            {
                var eleves = _metier.ListerEleves();
                return Ok(eleves);
            }
            catch (Exception ex)

            {
                return BadRequest($"Erreur lors de la recuperation des élèves : {ex.Message}");
            }




        }

        //GET api/eleves{id}


        [HttpGet("{id}")]
        public ActionResult<Eleve> GetEleveById(int id)
        {
            try
            {
                var eleve = _metier.RecupererUnEleve(id);
                return Ok(eleve);
            }
            catch (Exception ex)
            {
                return NotFound($"Erreur : {ex.Message}");
            }
        }


        //PUT api/eleves{id}

        [HttpPut("{id}")]
        public IActionResult MettreAJourEleve(int id, [FromBody] Eleve eleve)
        {
            if (eleve == null)
                return BadRequest("Données invalides");
            try
            {
                _metier.MettreAJourEleve(id, eleve.Code, eleve.Conduite);
                return Ok($"Eleve {id} mis à jour avec succes");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la mise a jour de l'eleve : {ex.Message}");
            }
        }


        //DELETE api/eleves{id}
        [HttpDelete("{id}")]
        public IActionResult SupprimerEleve(int id)
        {
            try
            {
                _metier.SupprimerEleve(id);
                return Ok($"Eleve {id} supprimé avec succes");
            }
            catch(Exception ex)
            {
                return BadRequest($"Erreur lors de la suppression de l'eleve : {ex.Message}");

            }
        }
    }
}
