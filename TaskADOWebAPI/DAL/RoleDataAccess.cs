using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using TaskADOWebAPI.Models;

namespace TaskADOWebAPI.DAL
{
    public class RoleDataAccess
    {
        private readonly SqlConnection _con;
        public RoleDataAccess()
        {
            _con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDBAccess"].ConnectionString);
        }

        public List<Role> GetAllRoles()
        {
            _con.Open();
            List<Role> roles = new List<Role>();


            SqlCommand cmd = new SqlCommand("spGetAllRoles", _con);
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
                Role cur = new Role();
                cur.Id = Convert.ToInt32(row["ID"]);
                cur.RoleName = Convert.ToString(row["RoleName"]);

                roles.Add(cur);

            }

            return roles;

        }

        public Role GetRoleById(int id)
        {
            List<Role> roles = GetAllRoles();

            Role role = roles.FirstOrDefault(x => x.Id == id);

            return role;
        }

        public int CreateRole(Role role)
        {
            _con.Open();
            SqlCommand cmd = new SqlCommand("spInsertRole", _con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@RoleName", role.RoleName);

            cmd.ExecuteNonQuery();
            _con.Close();

            return role.Id;
        }

        public bool UpdateRole(Role role)
        {
            if (role == null)
            {
                return false;
            }

            bool isPresent = GetAllRoles().Any(x => x.Id == role.Id);
            if (!isPresent) { return false; }

            _con.Open();
            SqlCommand cmd = new SqlCommand("spUpdateRole", _con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", role.Id);
            cmd.Parameters.AddWithValue("@RoleName", role.RoleName);
            cmd.ExecuteNonQuery();
            _con.Close();


            return true;

        }

        public bool DeleteRole(int id)
        {
            bool isIdPresent = GetAllRoles().Any(x => x.Id == id);
            if (!isIdPresent)
            {
                return false;
            }
            _con.Open();
            SqlCommand cmd = new SqlCommand("spDeleteRole", _con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            _con.Close();
            return true;
        }
    }
}