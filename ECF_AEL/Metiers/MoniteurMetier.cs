using ECF_AEL.Models;
using ECF_AEL.Repositories;

namespace ECF_AEL.Metier
{
    public class MoniteurMetier
    {
        private readonly MoniteurRepository _repo;

        public MoniteurMetier(string cs)
        {
            _repo = new MoniteurRepository(cs);
        }

        public void AjouterMoniteur(Moniteur moniteur)
        {

            if (moniteur == null)
                throw new ArgumentException("Les données du moniteur sont invalides.");

            if (string.IsNullOrWhiteSpace(moniteur.NomMoniteur))
                throw new ArgumentException("Le nom du moniteur est obligatoire.");

            if (moniteur.NomMoniteur.Length > 50)
                throw new ArgumentException("Le nom du moniteur ne doit pas dépasser 50 caractères.");

            if (string.IsNullOrWhiteSpace(moniteur.PrenomMoniteur))
                throw new ArgumentException("Le prénom du moniteur est obligatoire.");

            if (moniteur.PrenomMoniteur.Length > 50)
                throw new ArgumentException("Le prénom du moniteur ne doit pas dépasser 50 caractères.");

            if (moniteur.DateNaissance == DateTime.MinValue)
                throw new ArgumentException("La date de naissance est obligatoire.");

            if (moniteur.DateNaissance > DateTime.Now.AddYears(-18))
                throw new ArgumentException("Le moniteur doit avoir au moins 18 ans.");

            if (moniteur.DateEmbauche == DateTime.MinValue)
                throw new ArgumentException("La date d'embauche est obligatoire.");

            if (moniteur.DateEmbauche > DateTime.Now)
                throw new ArgumentException("La date d'embauche ne peut pas être dans le futur.");

            _repo.AjouterUnMoniteur(moniteur);
        }

        public List<Moniteur> ListerMoniteurs()
        {
            return _repo.ListerLesMoniteurs();
        }

        public void MettreAJourActivite(int id, bool nouvelleActivite)
        {

            if (id <= 0)
                throw new ArgumentException("Id moniteur invalide.");

            _repo.MettreAJourActivite(id, nouvelleActivite);
        }

        public void SupprimerMoniteur(int id)
        {
            _repo.SupprimerUnMoniteur(id);
        }
    }
}
