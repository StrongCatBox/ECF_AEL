using Microsoft.Data.SqlClient;

namespace ECF_AEL
{
    public class Connexion
    {
        private static string? _connectionString;
        public Connexion()
        {
            var envCs = Environment.GetEnvironmentVariable("AEL_DB");
            if (!string.IsNullOrEmpty(envCs))
            {
                _connectionString = envCs;
            }
            else
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                    .Build();

                _connectionString = config.GetConnectionString("AELdb");
            }
        }

        public SqlConnection GetConnection() {

            if (string.IsNullOrEmpty(_connectionString))
                throw new InvalidOperationException("la chaine de connexion n'a pas été définie.");
            return new SqlConnection( _connectionString );

        
        }
    }
}
