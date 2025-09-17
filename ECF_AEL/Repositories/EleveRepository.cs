using ECF_AEL.Models;
using Microsoft.Data.SqlClient;


namespace ECF_AEL.Repositories
{
    public class EleveRepository
    {
        private readonly string _cs;

        public EleveRepository(string connectionString)
        {
            _cs = connectionString;
        }


        //ajouter un eleve
        public void AjouterUnEleve(Eleve eleve) {

            using var conn = new SqlConnection(_cs);
                conn.Open();

            int newId = GetLastId();


            var sql = @"INSERT INTO ELEVE ([id élève],[nom élève], [prénom élève], [code], conduite, [date naissance]) VALUES (@id, @nom,@prenom, @code, @conduite, @date)";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", newId);
            cmd.Parameters.AddWithValue("@nom", eleve.NomEleve);
            cmd.Parameters.AddWithValue("@prenom", eleve.PrenomEleve);
            cmd.Parameters.AddWithValue("@code", eleve.Code);
            cmd.Parameters.AddWithValue("@conduite", eleve.Conduite);
            cmd.Parameters.AddWithValue("@date", eleve.DateNaissance);

            cmd.ExecuteNonQuery();
            
        }

        //lister tous les élèves

        public List<Eleve> ListerLesEleves()
        {
            var list = new List<Eleve>();
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = @"SELECT [id élève], [nom élève], [prénom élève], [code], conduite, [date naissance] FROM ELEVE";
            using var cmd = new SqlCommand(sql, conn);
            using var rd = cmd.ExecuteReader();

            while (rd.Read()) {
                var eleve = new Eleve
                {
                    IdEleve = rd.GetInt32(0),
                    NomEleve = rd.GetString(1),
                    PrenomEleve = rd.GetString(2),
                    Code = rd.GetBoolean(3),
                    Conduite = rd.GetBoolean(4),
                    DateNaissance = rd.GetDateTime(5)


                };
                list.Add(eleve);
            
            }
            return list;
        }

        //recuperre le dernier id et incmenter
        public int GetLastId()
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = "SELECT ISNULL(MAX([id élève]), 0) FROM ELEVE";
            using var cmd = new SqlCommand(sql, conn);

            int lastId = Convert.ToInt32(cmd.ExecuteScalar());
            return lastId + 1;
        }


        //Recuperer un eleve

        public Eleve GetById(int id )
        {
          
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = @"SELECT [id élève], [nom élève], [prénom élève], [code], conduite, [date naissance] FROM ELEVE WHERE [id élève] = @id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                return new Eleve
                {
                    IdEleve = rd.GetInt32(0),
                    NomEleve = rd.GetString(1),
                    PrenomEleve = rd.GetString(2),
                    Code = rd.GetBoolean(3),
                    Conduite = rd.GetBoolean(4),
                    DateNaissance = rd.GetDateTime(5)


                };
               

            }
            throw new Exception("Eleve introuvable");
        }


        // mettre a jour un eleve

        public void MettreAJourEleve(int idEleve, bool nouvCode, bool nouvConduite)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = "UPDATE ELEVE SET [code] = @code, conduite = @conduite WHERE [id élève] = @id";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@code", nouvCode);
            cmd.Parameters.AddWithValue("@conduite", nouvConduite);
            cmd.Parameters.AddWithValue("@id", idEleve);

            cmd.ExecuteNonQuery();
        }


        // supprimer un eleve
        public void SupprimerEleve(int idEleve)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = "DELETE FROM ELEVE WHERE [id élève] = @id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", idEleve);
            cmd.ExecuteNonQuery();

        }


        ////lister les eleves qui ont reussi

        //public List<Eleve> ListerReussites()
        //{
        //    var list = new List<Eleve>();
        //    using var conn = new SqlConnection(_cs);
        //    conn.Open();

        //    var sql = @"SELECT [id élève], [nom élève], [prénom élève], [code], conduite, [date naissance] FROM ELEVE WHERE code = true OR conduite = true";
        //    using var cmd = new SqlCommand(sql, conn);
        //    using var rd = cmd.ExecuteReader();

        //    while (rd.Read())
        //    {
        //        var eleve = new Eleve
        //        {
        //            IdEleve = rd.GetInt32(0),
        //            NomEleve = rd.GetString(1),
        //            PrenomEleve = rd.GetString(2),
        //            Code = rd.GetBoolean(3),
        //            Conduite = rd.GetBoolean(4),
        //            DateNaissance = rd.GetDateTime(5)


        //        };
        //        list.Add(eleve);

        //    }
        //    return list;
        //}


        ////lister les eleves qui ont échoué

        //public List<Eleve> ListerEchecs()
        //{
        //    var list = new List<Eleve>();
        //    using var conn = new SqlConnection(_cs);
        //    conn.Open();

        //    var sql = @"SELECT [id élève] [nom élève], [prénom élève], [code], conduite, [date naissance] FROM ELEVE WHERE conduite = false";
        //    using var cmd = new SqlCommand(sql, conn);
        //    using var rd = cmd.ExecuteReader();

        //    while (rd.Read())
        //    {
        //        var eleve = new Eleve
        //        {
        //            IdEleve = rd.GetInt32(0),
        //            NomEleve = rd.GetString(1),
        //            PrenomEleve = rd.GetString(2),
        //            Code = rd.GetBoolean(3),
        //            Conduite = rd.GetBoolean(4),
        //            DateNaissance = rd.GetDateTime(5)


        //        };
        //        list.Add(eleve);

        //    }
        //    return list;
        //}





    }
}
