using ECF_AEL.Models;
using Microsoft.Data.SqlClient;

namespace ECF_AEL.Repositories
{
    public class ModeleRepository
    {
        private readonly string _cs;

        public ModeleRepository(string connectionString)
        {
            _cs = connectionString;
        }

        public List<ModeleVehicule> ListerLesModeles()
        {
            var list = new List<ModeleVehicule>();
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = "SELECT [modèle véhicule], marque, année, [date achat] FROM MODELE";
            using var cmd = new SqlCommand(sql, conn);
            using var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                var modele = new ModeleVehicule
                {
                    NomModeleVehicule = rd.GetString(0),
                    Marque = rd.GetString(1),
                    Annee = rd.GetString(2).Trim(),
                    DateAchat = rd.GetDateTime(3)
                };
                list.Add(modele);
            }

            return list;
        }



        //ajouter un modele
        public void AjouterUnModele(ModeleVehicule modeleVehicule)
        {

            using var conn = new SqlConnection(_cs);
            conn.Open();


            var sql = @"INSERT INTO MODELE ([modèle véhicule], marque, [année], [date achat]) VALUES (@modelevehicule, @marque, @annee, @dateachat)";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@modelevehicule", modeleVehicule.NomModeleVehicule);
            cmd.Parameters.AddWithValue("@marque", modeleVehicule.Marque);
            cmd.Parameters.AddWithValue("@annee", modeleVehicule.Annee);
            cmd.Parameters.AddWithValue("@dateachat", modeleVehicule.DateAchat);


            cmd.ExecuteNonQuery();

        }
    }
}

