using Azure;
using ECF_AEL.Models;
using Microsoft.Data.SqlClient;

namespace ECF_AEL.Repositories
{
    public class VehiculeRepository
    {
        private readonly string _cs;

        public VehiculeRepository(string connectionString)
        {
            _cs = connectionString;
        }

        //ajouter un vehicule
        public void AjouterUnVehicule(Vehicule vehicule)
        {

            using var conn = new SqlConnection(_cs);
            conn.Open();


            var sql = @"INSERT INTO VEHICULE ([n°immatriculation], [modèle véhicule], [état]) VALUES (@numimmatric, @modelevehicule, @etatvehicule)";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@numimmatric", vehicule.NumeroImmatriculation);
            cmd.Parameters.AddWithValue("@modelevehicule", vehicule.ModeleVehicule);
            cmd.Parameters.AddWithValue("@etatvehicule", vehicule.Etat);


            cmd.ExecuteNonQuery();

        }



        //lister tous les vehicules

        public List<Vehicule> ListerLesVehicules()
        {
            var list = new List<Vehicule>();
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = @"SELECT [n°immatriculation], [modèle véhicule], [état] FROM VEHICULE";
            using var cmd = new SqlCommand(sql, conn);
            using var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                var vehicule = new Vehicule
                {
                    NumeroImmatriculation = rd.GetString(0),
                    ModeleVehicule = rd.GetString(1),
                    Etat = rd.GetBoolean(2),



                };
                list.Add(vehicule);

            }
            return list;
        }



        //Recuperer un vehicule

        public Vehicule GetByModele(string modele)
        {

            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = @"SELECT [n°immatriculation], [modèle véhicule], [état] FROM VEHICULE WHERE [modèle véhicule] = @modele";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@modele", modele);

            using var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                return new Vehicule
                {
                    NumeroImmatriculation = rd.GetString(0),
                    ModeleVehicule = rd.GetString(1),
                    Etat = rd.GetBoolean(2),


                };


            }
            throw new Exception("Vehicule introuvable");
        }



        // mettre a jour etat vehicule

        public void MettreAJourEtat(string NumeroImmatriculation, bool nouveauEtat)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();

            var sql = "UPDATE VEHICULE SET [état] = @etatvehicule WHERE [n°immatriculation] = @immatric";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@etatvehicule", nouveauEtat);
            cmd.Parameters.AddWithValue("@immatric", NumeroImmatriculation);

            cmd.ExecuteNonQuery();
        }


        // supprimer un vehicule
        public void SupprimerVehicule(string NumeroImmatriculation)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = "DELETE FROM VEHICULE WHERE [n°immatriculation] = @immatric";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@immatric", NumeroImmatriculation);
            cmd.ExecuteNonQuery();

        }
    }
}