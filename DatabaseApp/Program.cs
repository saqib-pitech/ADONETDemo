using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DatabaseApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------- disconnected data - dept table-------------");
            DisconnectedData();
            Console.WriteLine("------- connected data - emp table-------------");
            ConnectedData();
            Console.ReadLine();
        }
        static void DisconnectedData()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
            //SqlConnection connection = new SqlConnection(@"data source=(localdb)\MSSQLLocalDB;intial catalogue=training;integrated security=true;");
            string sql = "SELECT * FROM DEPT";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "dept");
            // print
            foreach(DataRow row in ds.Tables["dept"].Rows)
            {
                Console.WriteLine($"DeptNo: {row["DeptNo"]}\tDeptName: {row["Dname"]}\tLocation: {row["Loc"]}");
            }
        }
        static void ConnectedData()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
            string sql = "SELECT * FROM emp";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    
                    Console.WriteLine($"Empname: {reader["ename"]}\tSalary: {reader["sal"]}");
                }
                reader.Close();
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            

            // print
            //foreach (DataRow row in ds.Tables["dept"].Rows)
            //{
            //    Console.WriteLine($"DeptNo: {row["DeptNo"]}\tDeptName: {row["Dname"]}\tLocation: {row["Loc"]}");
            //}
        }
    }
}
