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
    class EMPCRUDDemo
    {
        EmpDataStore empDataStore;
        public EMPCRUDDemo()
        {
            empDataStore = new EmpDataStore(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);            
        }
        void DisplayAllEmps()
        {
            List<Emp> empList = empDataStore.GetEmps();
            foreach(Emp item in empList)
            {
                Console.WriteLine(item);
            }
        }
        void DisplayEmp(int eno)
        {
            try
            {
                Emp emp = empDataStore.GetEmp(eno);
                Console.WriteLine(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        void InsertEmp()
        {
            try
            {
                Emp emp = new Emp();
                Console.Write("Eno: ");
                emp.EmpNo = int.Parse(Console.ReadLine());
                Console.Write("Name: ");
                emp.EmpName = Console.ReadLine();
                Console.Write("Date: ");
                emp.HireDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Sal: ");
                emp.Salary = decimal.Parse(Console.ReadLine());
                //Console.WriteLine(emp);
                //Emp emp = new Emp() { EmpNo=124, EmpName="absd", HireDate=DateTime.Parse("01/01/2020"), Salary=1424};
                int temp=empDataStore.InsertEmp_SP(emp);
                Console.WriteLine(temp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        void DeleteEmpTest(int eno)
        {
            try
            {
                int temp = empDataStore.DeleteEmp(eno);
                Console.WriteLine(temp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        void UpdateEmpTest(int eno)
        {
            try
            {
                DeleteEmpTest(eno);
                InsertEmp();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void Main(String[] args)
        {
            EMPCRUDDemo empcrudDemo = new EMPCRUDDemo();
            //empcrudDemo.DisplayAllEmps();
            //empcrudDemo.DisplayEmp(1);
            //empcrudDemo.InsertEmp();
            //empcrudDemo.DeleteEmpTest(101);            
            empcrudDemo.UpdateEmpTest(6);
            Console.ReadLine();
        }
    }
}
