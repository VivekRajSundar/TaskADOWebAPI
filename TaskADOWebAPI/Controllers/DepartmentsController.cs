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
    public class DepartmentsController : ApiController
    {
        private readonly DepartmentDataAccess _db = new DepartmentDataAccess();


        [HttpGet]
        [Route("api/departments/getAllDepartments")]
        public IHttpActionResult Get()
        {
            return Ok(_db.GetAllDepartments());
        }

        [HttpGet]
        [Route("api/departments/getById/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(_db.GetDepartmentById(id));
        }

        [HttpPost]
        [Route("api/departments/create/")]
        public IHttpActionResult Post(Department department)
        {
            int newId = _db.CreateDepartment(department);

            return CreatedAtRoute($"api/employees/getById/{department.Id}", new { id = newId }, department);
        }

        [HttpPut]
        [Route("api/departments/update")]
        public IHttpActionResult Put(Department department)
        {
            bool result = _db.UpdateDepartment(department);
            if (!result) return NotFound();
            return Ok("Updated successfully");
        }

        [HttpDelete]
        [Route("api/departments/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            bool result = _db.DeleteDepartment(id);
            if (!result) return NotFound();
            return Ok("deleted successfully");
        }
    }
}
