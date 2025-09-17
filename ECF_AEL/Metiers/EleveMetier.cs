using ECF_AEL.Models;
using ECF_AEL.Repositories;

namespace ECF_AEL.Metier
{
    public class EleveMetier
    {
        private readonly EleveRepository _repo;

        public EleveMetier(string cs)
        {
            _repo = new EleveRepository(cs);
        }

        public void AjouterUnEleve(Eleve eleve)
        {

            if (eleve.NomEleve.Length > 50)
                throw new ArgumentException("Le nom de l'élève ne doit pas dépasser 50 caractères.");

            if (eleve.PrenomEleve.Length > 50)
                throw new ArgumentException("Le prénom de l'élève ne doit pas dépasser 50 caractères.");


            if (eleve == null)
                throw new ArgumentException("L'élève ne peut pas être null.");

            if (string.IsNullOrEmpty(eleve.NomEleve))
                throw new ArgumentException("Le nom de l'élève est obligatoire.");

            if (string.IsNullOrEmpty(eleve.PrenomEleve))
                throw new ArgumentException("Le prénom de l'élève est obligatoire.");

            if (eleve.DateNaissance > DateTime.Now.AddYears(-16)) 
                throw new ArgumentException("L'élève doit avoir au moins 16 ans.");
            _repo.AjouterUnEleve(eleve);
        }

        public List<Eleve> ListerEleves()
        {
            return _repo.ListerLesEleves();
        }

        public void MettreAJourEleve(int id, bool code, bool conduite)
        {

            _repo.MettreAJourEleve(id, code, conduite);
        }

        public void SupprimerEleve(int id)
        {
            {
                if (id <= 0)
                    throw new ArgumentException("Id invalide pour la suppression.");

                _repo.SupprimerEleve(id);
            }
        }

        public Eleve RecupererUnEleve(int id)
        {

            if (id <= 0)
                throw new ArgumentException("Id invalide.");
            return _repo.GetById(id);
        }

    }
}
