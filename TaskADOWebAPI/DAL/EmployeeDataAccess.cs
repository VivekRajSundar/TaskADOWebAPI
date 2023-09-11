using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using TaskADOWebAPI.Models;

namespace TaskADOWebAPI.DAL
{
    public class EmployeeDataAccess
    {
        private readonly SqlConnection _con;
        public EmployeeDataAccess()
        {
            _con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDBAccess"].ConnectionString);
        }

        public List<Employee> GetAllEmployees()
        {
            _con.Open();
            List<Employee> employees = new List<Employee>();

            
                SqlCommand cmd = new SqlCommand("spGetAllEmployees", _con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                _con.Close();
                da.Fill(ds);

                DataTable dt = ds.Tables[0];
            // LINQ for returning List type 
            foreach (DataRow row in dt.Rows)
            {
                Employee cur = new Employee();
                cur.Id = Convert.ToInt32(row["ID"]);
                cur.FirstName = Convert.ToString(row["FirstName"]);
                cur.LastName = Convert.ToString(row["LastName"]);
                cur.EmailId = Convert.ToString(row["EmailId"]);
                cur.Gender = Convert.ToString(row["Gender"]);
                cur.PhoneNumber = Convert.ToString(row["PhoneNumber"]);
                cur.Salary = Convert.ToDecimal(row["Salary"]);
                cur.DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]);
                cur.DateOfJoining = Convert.ToDateTime(row["DateOfJoining"]);
                cur.DeptId = Convert.ToInt32(row["DeptId"]);
                if(row["ProjectId"].ToString() != "") cur.ProjectId = Convert.ToInt32(row["ProjectId"]);
                cur.RoleId = Convert.ToInt32(row["RoleId"]);
                if(row["ManagerId"].ToString() != "")  cur.ManagerId = Convert.ToInt32(row["ManagerId"]);
                cur.IsActive = Convert.ToBoolean(row["IsActive"]);
                if (row["DateOfResigning"].ToString() != "") cur.DateOfResigning = Convert.ToDateTime(row["DateOfResigning"]);
                if (row["UpdatedDate"].ToString() != "") cur.UpdatedDate = Convert.ToDateTime(row["UpdatedDate"]);
                employees.Add(cur);

            }
              
            return employees;

        }

        public Employee GetEmployeeById(int id)
        {
            List<Employee> employees = GetAllEmployees();
            
            Employee emp = employees.FirstOrDefault(x => x.Id == id);

            return emp;
        }

        public int CreateEmployee(Employee employee)
        {
            _con.Open();
            SqlCommand cmd = new SqlCommand("spInsertEmployee", _con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employee.LastName);
            cmd.Parameters.AddWithValue("@EmailId", employee.EmailId);
            cmd.Parameters.AddWithValue("@Gender", employee.Gender);
            cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            cmd.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
            cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
            cmd.Parameters.AddWithValue("@DateOfResigning", employee.DateOfResigning);
            cmd.Parameters.AddWithValue("@UpdatedDate", employee.UpdatedDate);
            cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
            cmd.Parameters.AddWithValue("@ManagerId", employee.ManagerId);
            cmd.Parameters.AddWithValue("@DeptId", employee.DeptId);
            cmd.Parameters.AddWithValue("@ProjectId", employee.ProjectId);
            cmd.Parameters.AddWithValue("@RoleId", employee.RoleId);
            cmd.ExecuteNonQuery();
            _con.Close();

            return employee.Id;
        }

        public bool UpdateEmployee(Employee employee)
        {
            if (employee == null)
            {
                return false;
            }

            bool isPresent = GetAllEmployees().Any(x =>  x.Id == employee.Id);
            if (!isPresent) { return false; }

            _con.Open();
            SqlCommand cmd = new SqlCommand("spUpdateEmployee", _con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", employee.Id);
            cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employee.LastName);
            cmd.Parameters.AddWithValue("@EmailId", employee.EmailId);
            cmd.Parameters.AddWithValue("@Gender", employee.Gender);
            cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            cmd.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
            cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
            cmd.Parameters.AddWithValue("@DateOfResigning", employee.DateOfResigning);
            cmd.Parameters.AddWithValue("@UpdatedDate", employee.UpdatedDate);
            cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
            cmd.Parameters.AddWithValue("@ManagerId", employee.ManagerId);
            cmd.Parameters.AddWithValue("@DeptId", employee.DeptId);
            cmd.Parameters.AddWithValue("@ProjectId", employee.ProjectId);
            cmd.Parameters.AddWithValue("@RoleId", employee.RoleId);
            cmd.ExecuteNonQuery();
            _con.Close();


            return true;

        }

        public bool DeleteEmployee(int id)
        {
            bool isIdPresent = GetAllEmployees().Any(x => x.Id == id);
            if (!isIdPresent)
            {
                return false;
            }
            _con.Open();
            SqlCommand cmd = new SqlCommand("spDeleteEmployee", _con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            _con.Close();
            return true;
        }

    }
}