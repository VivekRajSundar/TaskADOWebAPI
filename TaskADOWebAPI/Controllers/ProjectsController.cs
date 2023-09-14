using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskADOWebAPI.DAL;
using TaskADOWebAPI.Models;

namespace TaskADOWebAPI.Controllers
{
    public class ProjectsController : ApiController
    {
        private readonly ProjectDataAccess _db = new ProjectDataAccess();


        [HttpGet]
        [Route("api/projects/getAllProjects")]
        public IHttpActionResult Get()
        {
            return Ok(_db.GetAllProjects());
        }

        [HttpGet]
        [Route("api/projects/getById/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(_db.GetProjectById(id));
        }

        [HttpPost]
        [Route("api/projects/create/")]
        public IHttpActionResult Post(Project project)
        {
            int newId = _db.CreateProject(project);

            return Created($"api/employees/getById/{newId}", project);
        }

        [HttpPut]
        [Route("api/projects/update")]
        public IHttpActionResult Put(Project project)
        {
            bool result = _db.UpdateProject(project);
            if (!result) return NotFound();
            return Ok("Updated successfully");
        }

        [HttpDelete]
        [Route("api/projects/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            bool result = _db.DeleteProject(id);
            if (!result) return NotFound();
            return Ok("deleted successfully");
        }
    }
}
