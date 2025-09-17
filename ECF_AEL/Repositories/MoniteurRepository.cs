using ECF_AEL.Models;
using Microsoft.Data.SqlClient;

namespace ECF_AEL.Repositories

{
    public class MoniteurRepository
    {
        private readonly string _cs;

        public MoniteurRepository(string connectionString)
        {
            _cs = connectionString;
        }

        //ajouter un moniteur
        public void AjouterUnMoniteur(Moniteur moniteur)
        {

            using var conn = new SqlConnection(_cs);
            conn.Open();

            int newId = GetLastId();


            var sql = @"INSERT INTO MONITEUR ([id moniteur], [nom moniteur], [prénom moniteur], [date naissance], [date embauche], [activité]) VALUES (@id, @nom, @prenom, @datenaissance, @dateembauche, @activite)";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", newId);
            cmd.Parameters.AddWithValue("@nom", moniteur.NomMoniteur);
            cmd.Parameters.AddWithValue("@prenom", moniteur.PrenomMoniteur);
            cmd.Parameters.AddWithValue("@datenaissance", moniteur.DateNaissance);
            cmd.Parameters.AddWithValue("@dateembauche", moniteur.DateEmbauche);
            cmd.Parameters.AddWithValue("@activite", moniteur.Activite);

            cmd.ExecuteNonQuery();

        }

        // recuperer dernier id et incrementer
        public int GetLastId()
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = "SELECT ISNULL(MAX([id moniteur]), 0) FROM MONITEUR";
            using var cmd = new SqlCommand(sql, conn);

            int lastId = Convert.ToInt32(cmd.ExecuteScalar());
            return lastId + 1;
        }

        //Recuperer un moniteur

        public Moniteur GetById(int id)
        {
         
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = @"SELECT [id moniteur], [nom moniteur], [prénom moniteur], [date naissance], [date embauche], [activité] FROM MONITEUR WHERE [id moniteur] = @id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                return new Moniteur
                {
                    IdMoniteur = rd.GetInt32(0),
                    NomMoniteur = rd.GetString(1),
                    PrenomMoniteur = rd.GetString(2),
                    DateNaissance = rd.GetDateTime(3),
                    DateEmbauche = rd.GetDateTime(4),
                    Activite = rd.GetBoolean(5)


                };


            }
            throw new Exception("Moniteur introuvable");
        }



        //lister tous les moniteurs

        public List<Moniteur> ListerLesMoniteurs()
        {
            var list = new List<Moniteur>();
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = @"SELECT [id moniteur], [nom moniteur], [prénom moniteur],[date naissance], [date embauche], [activité] FROM MONITEUR";
            using var cmd = new SqlCommand(sql, conn);
            using var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                var moniteur = new Moniteur
                {
                    IdMoniteur = rd.GetInt32(0),
                    NomMoniteur = rd.GetString(1),
                    PrenomMoniteur = rd.GetString(2),
                    DateNaissance = rd.GetDateTime(3),
                    DateEmbauche = rd.GetDateTime(4),
                    Activite = rd.GetBoolean(5)


                };
                list.Add(moniteur);

            }
            return list;
        }


        // mettre a jour activité

        public void MettreAJourActivite(int idMoniteur, bool nouvelleActivite)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = "UPDATE MONITEUR SET [activité] = @activite WHERE [id moniteur] = @id";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@activite", nouvelleActivite);
            cmd.Parameters.AddWithValue("@id", idMoniteur);

            cmd.ExecuteNonQuery();
        }


        // supprimer un moniteur
        public void SupprimerUnMoniteur(int IdMoniteur)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = "DELETE FROM MONITEUR WHERE [id moniteur] = @id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", IdMoniteur);
            cmd.ExecuteNonQuery();

        }
    }
}

