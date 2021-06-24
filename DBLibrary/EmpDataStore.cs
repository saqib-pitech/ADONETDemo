using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DBLibrary
{
    public class EmpDataStore
    {
        // CRUD operations with connected arch
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataReader reader = null;

        public EmpDataStore(string connectionString)
        {
            connection = new SqlConnection(connectionString); 
        }
        // get all emps detail
        public List<Emp> GetEmps()
        {
            try
            {
                string sql = "select empno, ename, hiredate, sal from emp";
                command = new SqlCommand(sql, connection);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                reader = command.ExecuteReader();
                List<Emp> empList = new List<Emp>();
                while (reader.Read())
                {
                    Emp emp = new Emp();
                    emp.EmpNo = (int)reader["empno"];
                    emp.EmpName = reader["ename"].ToString();
                    emp.HireDate = reader["hiredate"] as DateTime?;
                    emp.Salary = reader["sal"] as decimal?;
                    empList.Add(emp);
                }
                reader.Close();
                return empList;
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
        // get emp info by empno
        public Emp GetEmp(int eno)
        {
            try
            {
                //string sql = $"select empno, ename, hiredate, sal from emp where empno={eno}";
                string sql = "select empno, ename, hiredate, sal from emp where empno=@eno";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("eno", eno);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                reader = command.ExecuteReader();                //reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                }
                Emp emp = new Emp();
                emp.EmpNo = (int)reader["empno"];
                emp.EmpName = reader["ename"].ToString();
                emp.HireDate = reader["hiredate"] as DateTime?;
                emp.Salary = reader["sal"] as decimal?;
                reader.Close();
                return emp;
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
        // create method which insert emp record
        // take all the req values from the user for emp
        public int InsertEmp(Emp emp)
        {
            try
            {
                //string sql = $"select empno, ename, hiredate, sal from emp where empno={eno}";
                string sql = "INSERT INTO EMP(empno, ename, hiredate, sal) VALUES(@ENO,@ENAME,@DATE,@SAL)";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("ENO", emp.EmpNo);
                command.Parameters.AddWithValue("ENAME", emp.EmpName);
                command.Parameters.AddWithValue("DATE", emp.HireDate);
                command.Parameters.AddWithValue("SAL", emp.Salary);
                foreach(SqlParameter param in command.Parameters)
                {
                    if (param.Value == null)
                    {
                        param.Value = DBNull.Value;
                    }
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                int count = command.ExecuteNonQuery();                //reader = command.ExecuteReader();                                
                return count;
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
        public int InsertEmp_SP(Emp emp)
        {
            try
            {
                //string sql = $"select empno, ename, hiredate, sal from emp where empno={eno}";
                //string sql = "INSERT INTO EMP(empno, ename, hiredate, sal) VALUES(@ENO,@ENAME,@DATE,@SAL)";
                command = new SqlCommand("INSERTEMP_SP", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ENO", emp.EmpNo);
                command.Parameters.AddWithValue("NAME", emp.EmpName);
                command.Parameters.AddWithValue("DATE", emp.HireDate);
                command.Parameters.AddWithValue("SAL", emp.Salary);  //@ENO, @NAME, @DATE, @SAL)
                foreach (SqlParameter param in command.Parameters)
                {
                    if (param.Value == null)
                    {
                        param.Value = DBNull.Value;
                    }
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                int count = command.ExecuteNonQuery();                //reader = command.ExecuteReader();                                
                return count;
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
        public int DeleteEmp(int eno)
        {
            try
            {
                //string sql = $"select empno, ename, hiredate, sal from emp where empno={eno}";
                string sql = "delete FROM EMP WHERE EMPNO=@ENO";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("ENO", eno);                
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                int count = command.ExecuteNonQuery();                //reader = command.ExecuteReader();                                
                return count;
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
    }
}
