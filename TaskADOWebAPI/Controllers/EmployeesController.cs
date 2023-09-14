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
    public class EmployeesController : ApiController
    {
        private readonly EmployeeDataAccess _db = new EmployeeDataAccess();


        [HttpGet]
        [Route("api/employees/getAllEmployees")]
        public IHttpActionResult Get()
        {
            return Ok(_db.GetAllEmployees());
        }

        [HttpGet]
        [Route("api/employees/getById/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(_db.GetEmployeeById(id));
        }

        [HttpGet]
        [Route("api/employees/getEmployeesByYOE/{year}")]
        public IHttpActionResult GetSeniors(int year)
        {
            return Ok(_db.GetSeniorEmployees(year));
        }

        [HttpPost]
        [Route("api/employees/create/")]
        public IHttpActionResult Post(Employee employee)
        {
            int newId = _db.CreateEmployee(employee);
            
            return Created($"api/employees/getById/{newId}", employee);
        }

        [HttpPut]
        [Route("api/employees/update")]
        public IHttpActionResult Put(Employee employee)
        {
            bool result = _db.UpdateEmployee(employee);
            if(!result) return NotFound();
            return Ok("Updated successfully");
        }

        [HttpDelete]
        [Route("api/employees/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            bool result = _db.DeleteEmployee(id);
            if (!result) return NotFound();
            return Ok("deleted successfully");
        }
    }
}
