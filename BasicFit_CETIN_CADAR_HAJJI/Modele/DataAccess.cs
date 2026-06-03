using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System.Data;
using Npgsql;

namespace BasicFit_CETIN_CADAR_HAJJI
{
    public class DataAccess
    {
        private static string connectionString;
        private static NpgsqlConnection connection;

        // CONNEXION AVEC BDD IUT
        public static void InitializeConnection(string username, string password)
        {
            connectionString = $"Host=srv-peda-new;Port=5433;Username={username};Password={password};Database=BasicFit_CADAR_CETIN_HAJJI;";
            try
            {
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb à la connexion\n");
                throw;
            }
        }

        // CONNEXION AVEC BDD LOCALE
        //public static void InitializeConnection(string username, string password)
        //{
        //    connectionString = $"Host=localhost;Port=5432;Username=postgres;Password={password};Database=BasicFit_CADAR_CETIN_HAJJI";
        //    try
        //    {
        //        connection = new NpgsqlConnection(connectionString);
        //        connection.Open();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError.Log(ex, "Pb à la connexion\n");
        //        throw;
        //    }
        //}

        public static NpgsqlConnection GetConnection()
        {
            if (connection == null)
                throw new InvalidOperationException("La connexion n'a pas été initialisée.");

            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    LogError.Log(ex, "Pb à la connexion\n");
                    throw;
                }

            return connection;
        }

        public static DataTable ExecuteSelect(NpgsqlCommand cmd)
        {
            DataTable dataTable = new DataTable();
            try
            {
                cmd.Connection = GetConnection();
                using (var adapter = new NpgsqlDataAdapter(cmd))
                    adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de executeSelect\n" + cmd.CommandText);
                throw;
            }
            return dataTable;
        }


        public static int ExecuteInsert(NpgsqlCommand cmd)
        {
            try
            {
                cmd.Connection = GetConnection();
                return (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de executeInsert\n" + cmd.CommandText);
                throw;
            }
        }

        public static int ExecuteSet(NpgsqlCommand cmd)
        {
            try
            {
                cmd.Connection = GetConnection();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de executeSet\n" + cmd.CommandText);
                throw;
            }
        }

        public static string ExecuteSelectOneValue(NpgsqlCommand cmd)
        {
            try
            {
                cmd.Connection = GetConnection();
                object res = cmd.ExecuteScalar();
                return res.ToString();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de ExecuteSelectOneValue\n" + cmd.CommandText);
                throw;
            }
        }

        public static void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}