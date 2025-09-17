using ECF_AEL.Models;
using ECF_AEL.Repositories;

namespace ECF_AEL.Metier
{
    public class VehiculeMetier
    {
        private readonly VehiculeRepository _repo;

        public VehiculeMetier(string cs)
        {
            _repo = new VehiculeRepository(cs);
        }

        public void AjouterVehicule(Vehicule vehicule)
        {

            if (vehicule == null)
                throw new ArgumentException("Les données du véhicule sont invalides.");

            if (string.IsNullOrWhiteSpace(vehicule.NumeroImmatriculation))
                throw new ArgumentException("Le numéro d'immatriculation est obligatoire.");

            if (vehicule.NumeroImmatriculation.Length > 9)
                throw new ArgumentException("Le numéro d'immatriculation ne doit pas dépasser 9 caractères.");

            if (string.IsNullOrWhiteSpace(vehicule.ModeleVehicule))
                throw new ArgumentException("Le modèle du véhicule est obligatoire.");

            if (vehicule.ModeleVehicule.Length > 50)
                throw new ArgumentException("Le modèle du véhicule ne doit pas dépasser 50 caractères.");

            _repo.AjouterUnVehicule(vehicule);
        }

        public List<Vehicule> ListerVehicules()
        {
            return _repo.ListerLesVehicules();
        }

        public void MettreAJourEtat(string immatric, bool nouvelEtat)
        {
            _repo.MettreAJourEtat(immatric, nouvelEtat);
        }

        public void SupprimerVehicule(string immatric)
        {
            _repo.SupprimerVehicule(immatric);
        }
    }
}
