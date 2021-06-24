using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseApp
{
    class ClientTest
    {
        UserDataStore userDataStore;
        public ClientTest()
        {
            userDataStore = new UserDataStore(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
        }
        bool ValidateUserTest()
        {
            try
            {
                UserData user = new UserData();
                Console.Write("Username: ");
                user.UserName = Console.ReadLine();
                Console.Write("Password: ");
                user.Password = Console.ReadLine();
                return userDataStore.ValidateUser(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        static bool ValidateDisconnected(UserData user)
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
            if (dr!=null && dr.Length!=0)
                return true;
            return false;
        }
        static void Main(string[] args)
        {
            //ClientTest clientTest = new ClientTest();
            //Console.WriteLine(  clientTest.ValidateUserTest());
            UserData user = new UserData() { UserName = "abc", Password = "pass" };
            Console.WriteLine(ValidateDisconnected(user));
            Console.ReadKey();
        }
    }
}
