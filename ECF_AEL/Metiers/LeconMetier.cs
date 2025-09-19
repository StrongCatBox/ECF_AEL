using ECF_AEL.Models;
using ECF_AEL.Repositories;

namespace ECF_AEL.Metiers
{
    public class LeconMetier
    {
        private readonly LeconRepository _leconRepository;
        private readonly EleveRepository _eleveRepository;
        private readonly MoniteurRepository _moniteurRepository;
        private readonly VehiculeRepository _vehiculeRepository;
        private readonly CalendrierRepository _calendrierRepository;

        public LeconMetier(string connectionString)
        {
            _leconRepository = new LeconRepository(connectionString);
            _eleveRepository = new EleveRepository(connectionString);
            _moniteurRepository = new MoniteurRepository(connectionString);
            _vehiculeRepository = new VehiculeRepository(connectionString);
            _calendrierRepository = new CalendrierRepository(connectionString);
        }

        public void ReserverUnLecon(Lecon lecon)
        {
            if (lecon == null)
                throw new ArgumentException("Les données de la leçon sont invalides.");

            // verifer champs obmigatoires
            if (lecon.IdEleve <= 0)
                throw new ArgumentException("Un élève valide est obligatoire pour réserver une leçon.");
            if (lecon.IdMoniteur <= 0)
                throw new ArgumentException("Un moniteur valide est obligatoire pour réserver une leçon.");
            if (string.IsNullOrWhiteSpace(lecon.ModeleVehicule))
                throw new ArgumentException("Un véhicule est obligatoire pour réserver une leçon.");
            if (lecon.DateHeure == DateTime.MinValue)
                throw new ArgumentException("La date/heure de la leçon est obligatoire.");
            if (lecon.Duree <= 0)
                throw new ArgumentException("La durée de la leçon doit être supérieure à zéro.");

            // Vérifie que le creneau existe dans le calendrier
            var creneaux = _calendrierRepository.ListerCreneaux();
            // Vérifie que le creneau existe sinon l'ajoute
            _calendrierRepository.AjouterCreneauSiAbsent(lecon.DateHeure);


            // Vérifie l'age de l'eleve
            var eleve = _eleveRepository.GetById(lecon.IdEleve);
            var age = DateTime.Today.Year - eleve.DateNaissance.Year;
            if (eleve.DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;
            if (age < 16)
                throw new Exception("L'élève doit avoir au moins 16 ans pour réserver une leçon.");

            // Vérifie la disponibilite du moniteur
            var moniteur = _moniteurRepository.GetById(lecon.IdMoniteur);
            if (!moniteur.Activite)
                throw new Exception("Ce moniteur est en congé et ne peut pas donner de leçon.");

            // Vérifie la disponibilité du véhicule
            var vehicule = _vehiculeRepository.GetByModele(lecon.ModeleVehicule);
            if (!vehicule.Etat)
                throw new Exception("Ce véhicule est en révision et ne peut pas être utilisé.");

           
            _leconRepository.ReserverUnLeçon(lecon);
        }

        public List<Lecon> ListerLecons()
        {
            return _leconRepository.ListerLesLecons();
        }
    }
}
