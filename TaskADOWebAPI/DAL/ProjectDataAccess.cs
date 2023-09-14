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
    public class ProjectDataAccess
    {
        private readonly SqlConnection _con;
        public ProjectDataAccess()
        {
            _con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDBAccess"].ConnectionString);
        }

        public List<Project> GetAllProjects()
        {
            _con.Open();
            List<Project> projects = new List<Project>();


            SqlCommand cmd = new SqlCommand("spGetAllProjects", _con);
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
                Project cur = new Project();
                cur.Id = Convert.ToInt32(row["ID"]);
                cur.ProjectName = Convert.ToString(row["ProjectName"]);

                projects.Add(cur);

            }

            return projects;

        }

        public Project GetProjectById(int id)
        {
            List<Project> departments = GetAllProjects();

            Project project = departments.FirstOrDefault(x => x.Id == id);

            return project;
        }

        public int CreateProject(Project project)
        {
            _con.Open();
            SqlCommand cmd = new SqlCommand("spInsertProject", _con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);

            var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();
            var id = returnParameter.Value;
            _con.Close();

            return (int)id;
        }

        public bool UpdateProject(Project project)
        {
            if (project == null)
            {
                return false;
            }

            bool isPresent = GetAllProjects().Any(x => x.Id == project.Id);
            if (!isPresent) { return false; }

            _con.Open();
            SqlCommand cmd = new SqlCommand("spUpdateProject", _con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", project.Id);
            cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
            cmd.ExecuteNonQuery();
            _con.Close();


            return true;

        }

        public bool DeleteProject(int id)
        {
            bool isIdPresent = GetAllProjects().Any(x => x.Id == id);
            if (!isIdPresent)
            {
                return false;
            }
            _con.Open();
            SqlCommand cmd = new SqlCommand("spDeleteProject", _con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            _con.Close();
            return true;
        }
    }
}