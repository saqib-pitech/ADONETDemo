using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;

namespace DBLibrary
{
    public class UserDataStore
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataReader reader = null;

        public UserDataStore(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }
        public bool ValidateUser(UserData user)
        {
            try
            {
                string sql = "SELECT * FROM USERDATA WHERE USERNAME=@UNAME AND PASSWORD=@PWD";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("UNAME", user.UserName);
                command.Parameters.AddWithValue("PWD", user.Password);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                reader = command.ExecuteReader();                //reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    //reader.Close();
                    return true;
                }                
                //reader.Close();
                return false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
        public bool ValidateDisconnected(UserData user)
        {
            // Disconnected Arch
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
            //SqlConnection connection = new SqlConnection(@"data source=(localdb)\MSSQLLocalDB;intial catalogue=training;integrated security=true;");
            string sql = "SELECT * FROM USERDATA";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "USERDATA");
            DataTable dataTable = ds.Tables["USERDATA"];
            //if (dataTable == null) Console.WriteLine("sldfkj");
            DataRow[] dr = dataTable.Select($"USERNAME='{user.UserName}' AND PASSWORD='{user.Password}'");
            //DataRow[] dr = dataTable.Select("USERNAME='dbc' AND PASSWORD='pass'");
            if (dr != null && dr.Length != 0)
                return true;
            return false;
        }
    }
}
