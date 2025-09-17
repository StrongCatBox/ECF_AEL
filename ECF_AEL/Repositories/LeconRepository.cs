using ECF_AEL.Models;
using Microsoft.Data.SqlClient;


namespace ECF_AEL.Repositories
{
    public class LeconRepository
    {

        private readonly string _cs;

        public LeconRepository(string connectionString)
        {
            _cs = connectionString;
        }



        //ajouter un leçon
        public void ReserverUnLeçon(Lecon lecon)
        {

            using var conn = new SqlConnection(_cs);
            conn.Open();


            // inserer la lecon

            var sql = @"INSERT INTO LECON ([modèle véhicule], [date heure], [id élève], [id moniteur], [durée]) VALUES (@modelevehicule,@dateheure, @ideleve, @idmoniteur, @duree)";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@modelevehicule", lecon.ModeleVehicule);
            cmd.Parameters.AddWithValue("@dateheure", lecon.DateHeure);
            cmd.Parameters.AddWithValue("@ideleve", lecon.IdEleve);
            cmd.Parameters.AddWithValue("@idmoniteur", lecon.IdMoniteur);
            cmd.Parameters.AddWithValue("@duree", lecon.Duree);

            cmd.ExecuteNonQuery();



        }


        //lister tous les lecons

        public List<Lecon> ListerLesLecons()
        {
            var list = new List<Lecon>();
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = @"SELECT [modèle véhicule], [date heure], [id élève], [id moniteur], [durée] FROM LECON";
            using var cmd = new SqlCommand(sql, conn);
            using var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                var lecon = new Lecon
                {
                    ModeleVehicule = rd.GetString(0),
                    DateHeure = rd.GetDateTime(1),
                    IdEleve = rd.GetInt32(2),
                    IdMoniteur = rd.GetInt32(3),
                    Duree = rd.GetInt32(4),


                };
                list.Add(lecon);

            }
            return list;
        }



    }
}
