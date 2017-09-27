using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace TestTVP
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creating a datatable with the same structure of the TVP 
            DataTable BatchEmployee = new DataTable();
            BatchEmployee.Columns.Add("Name", typeof(string));
            BatchEmployee.Columns.Add("Age", typeof(int));

            //Adding records into the datatable
            BatchEmployee.Rows.Add(new object[] { "Ahmed1", 27 });
            BatchEmployee.Rows.Add(new object[] { "Ahmed2", 28 });
            BatchEmployee.Rows.Add(new object[] { "Ahmed3", 29 });
            BatchEmployee.Rows.Add(new object[] { "Ahmed4", 30 });
            BatchEmployee.Rows.Add(new object[] { "Ahmed5", 31 });

            //Calling the stored procedure passing the datatable as a TVP
            //Modify the connection string to work with your environment before running the code
            using (var conn = new SqlConnection("Data Source=DEVMACHINE2;Initial Catalog=TvpTestDB;Integrated Security=True"))
            {
                conn.Open();
                using (var cmd = new SqlCommand("AddEmployees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var BatchEmployeeParameter = cmd.Parameters.AddWithValue("@BatchEmployees", BatchEmployee);
                    //This is a very important step
                    //You should set the SqlDbType property of the SqlParameter to "Structured"
                    BatchEmployeeParameter.SqlDbType = SqlDbType.Structured;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

            //Creating instance of the EmployeeCollection and populate some employee data
            EmployeeCollection employees = new EmployeeCollection();
            employees.Add(new Employee() { Name = "Mohamed1", Age = 20 });
            employees.Add(new Employee() { Name = "Mohamed2", Age = 21 });
            employees.Add(new Employee() { Name = "Mohamed3", Age = 22 });
            employees.Add(new Employee() { Name = "Mohamed4", Age = 23 });
            employees.Add(new Employee() { Name = "Mohamed5", Age = 24 });
            employees.Add(new Employee() { Name = "Mohamed6", Age = 25 });

            //Calling the stored procedure passing the EmployeeCollection as a TVP
            //To do so, you should first make sure that the EmployeeCollection implements the "IEnumerable<SqlDataRecord>" interface
            //Check the EmployeeCollection class definition below
            //Modify the connection string to work with your environment before running the code
            using (var conn1 = new SqlConnection("Data Source=DEVMACHINE2;Initial Catalog=TvpTestDB;Integrated Security=True"))
            {
                conn1.Open();
                using (var cmd = new SqlCommand("AddEmployees", conn1))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var BatchEmployeeParameter = cmd.Parameters.AddWithValue("@BatchEmployees", employees);
                    //This is a very important step
                    //You should set the SqlDbType property of the SqlParameter to "Structured"
                    BatchEmployeeParameter.SqlDbType = SqlDbType.Structured;
                    cmd.ExecuteNonQuery();
                }
                conn1.Close();
            }
        }
    }

    //Employee class
    public class Employee
    {
        int id;
        public int Id
        {
            get { return id; }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        int age;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
    }

    //EmployeeCollection class
    //For EmployeeCollection to be used as a TVP, it should implement the "IEnumerable<SqlDataRecord>" interface
    public class EmployeeCollection : List<Employee>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            SqlDataRecord record = new SqlDataRecord
                (
                    new SqlMetaData("Name", SqlDbType.Text),
                    new SqlMetaData("Age", SqlDbType.Int)
                );

            foreach(Employee emp in this)
            {
                record.SetSqlString(0, emp.Name);
                record.SetSqlInt32(1, emp.Age);
                yield return record;
            }
        }
    }
}
