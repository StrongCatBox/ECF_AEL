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

        public void AjouterModele(ModeleVehicule modele)
        {
            if (modele == null)
                throw new ArgumentException("Les données du modèle sont invalides.");

            if (string.IsNullOrWhiteSpace(modele.NomModeleVehicule))
                throw new ArgumentException("Le nom du modèle est obligatoire.");

            if (modele.NomModeleVehicule.Length > 50)
                throw new ArgumentException("Le nom du modèle ne doit pas dépasser 50 caractères.");

            if (string.IsNullOrWhiteSpace(modele.Marque))
                throw new ArgumentException("La marque est obligatoire.");

            if (modele.Marque.Length > 50)
                throw new ArgumentException("La marque ne doit pas dépasser 50 caractères.");


            if (modele.Annee.Length > 4)
                throw new ArgumentException("L'année ne doit pas dépasser 4 caractères.");

            if (modele.DateAchat == DateTime.MinValue)
                throw new ArgumentException("La date d'achat est obligatoire.");

            if (modele.DateAchat > DateTime.Now)
                throw new ArgumentException("La date d'achat ne peut pas être dans le futur.");

            _repo.AjouterUnModele(modele);
        }

    }
}
