using ECF_AEL.Models;
using ECF_AEL.Repositories;
using System.Reflection;

namespace ECF_AEL.Metiers
{
    public class ModeleMetier
    {
        private readonly ModeleRepository _repo;

        public ModeleMetier(string connectionString)
        {
            _repo = new ModeleRepository(connectionString);
        }

       
        public List<ModeleVehicule> ListerModeles()
        {
           
            return _repo.ListerLesModeles();
        }
    }
}
