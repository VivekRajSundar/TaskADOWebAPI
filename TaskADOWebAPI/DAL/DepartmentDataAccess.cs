using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TaskADOWebAPI.Models;

namespace TaskADOWebAPI.DAL
{
    public class DepartmentDataAccess
    {
        private readonly SqlConnection _con;
        public DepartmentDataAccess()
        {
            _con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDBAccess"].ConnectionString);
        }

        public List<Department> GetAllDepartments()
        {
            _con.Open();
            List<Department> departments = new List<Department>();


            SqlCommand cmd = new SqlCommand("spGetAllDepartments", _con);
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
                Department cur = new Department();
                cur.Id = Convert.ToInt32(row["ID"]);
                cur.DepartmentName = Convert.ToString(row["DepartmentName"]);
                
                departments.Add(cur);

            }

            return departments;

        }

        public Department GetDepartmentById(int id)
        {
            List<Department> departments = GetAllDepartments();

            Department dept = departments.FirstOrDefault(x => x.Id == id);

            return dept;
        }

        public int CreateDepartment(Department department)
        {
            _con.Open();
            SqlCommand cmd = new SqlCommand("spInsertDepartment", _con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();
            var id = returnParameter.Value;
            _con.Close();

            return (int)id;
        }

        
        public bool UpdateDepartment(Department department)
        {
            if (department == null)
            {
                return false;
            }

            bool isPresent = GetAllDepartments().Any(x => x.Id == department.Id);
            if (!isPresent) { return false; }

            _con.Open();
            SqlCommand cmd = new SqlCommand("spUpdateDepartment", _con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", department.Id);
            cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            cmd.ExecuteNonQuery();
            _con.Close();


            return true;

        }

        public bool DeleteDepartment(int id)
        {
            bool isIdPresent = GetAllDepartments().Any(x => x.Id == id);
            if (!isIdPresent)
            {
                return false;
            }
            _con.Open();
            SqlCommand cmd = new SqlCommand("spDeleteDepartment", _con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            _con.Close();
            return true;
        }
    }
}