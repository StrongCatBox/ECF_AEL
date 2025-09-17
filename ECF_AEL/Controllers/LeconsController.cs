using ECF_AEL.Repositories;
using Microsoft.AspNetCore.Mvc;
using ECF_AEL.Models;
using ECF_AEL.Metiers;

namespace ECF_AEL.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LeconsController : ControllerBase
    {

        private readonly LeconMetier _metier;
      

        public LeconsController(IConfiguration config)
        {
            var cs = config.GetConnectionString("AELdb");
            _metier = new LeconMetier(cs);
        }


        //POST api/lecons/reserver

        [HttpPost("reserver")]
        public IActionResult ReserverLecon([FromBody] Lecon lecon)
        {

            if (lecon == null)
                return BadRequest("Données de la lecon invalides.");


            try
            {
                _metier.ReserverUnLecon(lecon);
                return Ok("Lecon reservée avec success");
            }
            catch (Exception ex) {
                return BadRequest($"Erreur lors de la reservation : {ex.Message}");
            }

        }



        // GET api/lecons/liste

        [HttpGet("liste")]

        public ActionResult<IEnumerable<Lecon>> ListerLecons()
        {
            try { var lecons = _metier.ListerLecons(); return Ok(lecons); }
            catch (Exception ex) { return BadRequest($"Erreur lors de la récupération des leçons : {ex.Message}"); }
        }
    }
}
