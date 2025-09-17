using ECF_AEL.Repositories;

namespace ECF_AEL.Metiers
{
    public class CalendrierMetier
    {
        private readonly CalendrierRepository _repo;

        public CalendrierMetier(string connectionString)
        {
            _repo = new CalendrierRepository(connectionString);
        }

        // Récupérer tous les créneaux
        public List<DateTime> ListerCreneaux()
        {
            return _repo.ListerCreneaux();
        }

        // Ajouter un créneau 
        public void AjouterCreneau(DateTime dateHeure)
        {
            // Pas de créneau dans le passé
            if (dateHeure < DateTime.Now)
                throw new ArgumentException("Impossible d’ajouter un créneau dans le passé.");

            // Pas de créneau le dimanche
            if (dateHeure.DayOfWeek == DayOfWeek.Sunday)
                throw new ArgumentException("Impossible d’ajouter un créneau le dimanche.");

            //  Pas de créneau un jour férié
            if (EstJourFerie(dateHeure))
                throw new ArgumentException("Impossible d’ajouter un créneau un jour férié.");

            _repo.AjouterCreneau(dateHeure);
        }

        // Supprimer un créneau
        public void SupprimerCreneau(DateTime dateHeure)
        {
            _repo.SupprimerCreneau(dateHeure);
        }

        // Vérifie si une date est un jour férié fixe
        private bool EstJourFerie(DateTime date)
        {
            var joursFeries = new List<(int mois, int jour)>
            {
                (1, 1), 
                (5, 1),   
                (5, 8),   
                (7, 14), 
                (8, 15), 
                (11, 1), 
                (11, 11),
                (12, 25)  
            };

            return joursFeries.Any(j => j.mois == date.Month && j.jour == date.Day);
        }
    }
}
