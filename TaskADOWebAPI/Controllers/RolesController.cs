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
    public class RolesController : ApiController
    {
        private readonly RoleDataAccess _db = new RoleDataAccess();


        [HttpGet]
        [Route("api/roles/getAllRoles")]
        public IHttpActionResult Get()
        {
            return Ok(_db.GetAllRoles());
        }

        [HttpGet]
        [Route("api/roles/getById/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(_db.GetRoleById(id));
        }

        [HttpPost]
        [Route("api/roles/create/")]
        public IHttpActionResult Post(Role role)
        {
            int newId = _db.CreateRole(role);

            return CreatedAtRoute($"api/employees/getById/{role.Id}", new { id = newId }, role);
        }

        [HttpPut]
        [Route("api/roles/update")]
        public IHttpActionResult Put(Role role)
        {
            bool result = _db.UpdateRole(role);
            if (!result) return NotFound();
            return Ok($"Role Id {role.Id} Updated successfully");
        }

        [HttpDelete]
        [Route("api/roles/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            bool result = _db.DeleteRole(id);
            if (!result) return NotFound();
            return Ok($"Role Id {id} deleted successfully");
        }
    }
}
