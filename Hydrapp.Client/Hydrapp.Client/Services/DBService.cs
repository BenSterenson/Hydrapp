using Hydrapp.Client.Modules;
using System;
using System.Data.SqlClient;

namespace Hydrapp.Client.Services
{
    public class DBService
    {
        public int writeToDB(BandEntry entry)
        {
            String connectionString = "Server=tcp:hydrapp-tau.database.windows.net,1433;Initial Catalog=Hydrapp;Persist Security Info=False;User ID={hydrapp2016};Password={tau2016BsNw};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();


                //SqlConnection conn = new SqlConnection();
            
            //conn.ConnectionString = connectionString;
            try
            {
                //conn.Open();
            }
            catch (Exception e)
            {

            }
            return 1;
            

        }
    }
}
