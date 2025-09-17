using Microsoft.Data.SqlClient;

namespace ECF_AEL.Repositories
{
    public class CalendrierRepository
    {
        private readonly string _cs;

        public CalendrierRepository(string connectionString)
        {
            _cs = connectionString;
        }

        // Récupérer tous les créneaux disponibles
        public List<DateTime> ListerCreneaux()
        {
            var list = new List<DateTime>();
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = "SELECT [date heure] FROM CALENDRIER";
            using var cmd = new SqlCommand(sql, conn);
            using var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                list.Add(rd.GetDateTime(0));
            }

            return list;
        }

        // Ajouter un créneau
        public void AjouterCreneau(DateTime dateHeure)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = "INSERT INTO CALENDRIER ([date heure]) VALUES (@dateheure)";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@dateheure", dateHeure);

            cmd.ExecuteNonQuery();
        }

        // Supprimer un créneau
        public void SupprimerCreneau(DateTime dateHeure)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = "DELETE FROM CALENDRIER WHERE [date heure] = @dateheure";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@dateheure", dateHeure);

            cmd.ExecuteNonQuery();
        }


        // on verifie si le creneau est absent
        public void AjouterCreneauSiAbsent(DateTime dateHeure)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var checkSql = "SELECT COUNT(*) FROM CALENDRIER WHERE [date heure] = @date";
            using var checkCmd = new SqlCommand(checkSql, conn);
            checkCmd.Parameters.AddWithValue("@date", dateHeure);
            int existe = (int)checkCmd.ExecuteScalar();

            if (existe == 0)
            {
                var insertSql = "INSERT INTO CALENDRIER ([date heure]) VALUES (@date)";
                using var insertCmd = new SqlCommand(insertSql, conn);
                insertCmd.Parameters.AddWithValue("@date", dateHeure);
                insertCmd.ExecuteNonQuery();
            }
        }

    }
}
